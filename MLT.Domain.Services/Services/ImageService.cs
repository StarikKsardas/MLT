using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using MLT.Domain.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;



namespace MLT.Domain.Services.Services
{
    public class ImageService : IImageService
    {
        private const int MaxValueInPixel = 1500;
        private const int WorkDpi = 500;
        

        public ImageInfo Base64StringToImageInfo(string base64)
        {
            var buffer = Convert.FromBase64String(base64);
            var bitmap = new Bitmap(new MemoryStream(buffer));
            var result = BitmapToImageInfo(bitmap);
            return result;
        }

        public ImageInfo BitmapToImageInfo(Bitmap bitmap)
        {
            var Rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(Rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr Ptr = bmpData.Scan0;

            byte[] ByteWithStride = new byte[bmpData.Stride * bmpData.Height];
            Marshal.Copy(Ptr, ByteWithStride, 0, ByteWithStride.Length);

            byte[] OutByte = new byte[bmpData.Width * bmpData.Height];
            switch (bitmap.PixelFormat)
            {
                case PixelFormat.Format8bppIndexed:
                    {
                        for (int i = 0; i < bmpData.Height; i++)
                        {
                            Array.Copy(ByteWithStride, i * bmpData.Stride, OutByte, i * bmpData.Width, bmpData.Width);

                        }
                    }
                    break;
                case PixelFormat.Format24bppRgb:
                    {
                        for (int I = 0; I < bmpData.Height; I++)
                        {
                            for (int J = 0; J < bmpData.Width; J++)
                            {
                                int TempColor = Convert.ToInt32(Math.Truncate(
                                    (ByteWithStride[I * bmpData.Stride + (J * 3)] * 0.115) +
                                    (ByteWithStride[I * bmpData.Stride + (J * 3) + 1] * 0.258) +
                                    (ByteWithStride[I * bmpData.Stride + (J * 3) + 2] * 0.636)));
                                if (TempColor > 255)
                                {
                                    TempColor = 255;
                                }
                                OutByte[(I * bmpData.Width) + J] = Convert.ToByte(TempColor);
                            }
                        }
                    }
                    break;
                case PixelFormat.Format32bppArgb:
                    {
                        for (int I = 0; I < bmpData.Height; I++)
                        {
                            for (int J = 0; J < bmpData.Width; J++)
                            {
                                int TempColor = Convert.ToInt32(Math.Truncate(
                                    (ByteWithStride[I * bmpData.Stride + (J * 4)] * 0.115) +
                                    (ByteWithStride[I * bmpData.Stride + (J * 4) + 1] * 0.258) +
                                    (ByteWithStride[I * bmpData.Stride + (J * 4) + 2] * 0.636)));
                                if (TempColor > 255)
                                {
                                    TempColor = 255;
                                }
                                OutByte[I * bmpData.Width + J] = Convert.ToByte(TempColor);
                            }
                        }
                    }
                    break;
            }
            var imageInfo = new ImageInfo()
            {
                Image = OutByte,
                Width = bmpData.Width,
                Height = bmpData.Height
            };
            bitmap.UnlockBits(bmpData);
            return imageInfo;
        }        

        public ImageInfo NormalizeImage(ImageInfo imageInfo)
        {
            if ((imageInfo.Width < MaxValueInPixel) || (imageInfo.Height < MaxValueInPixel))
            {
                return imageInfo;
            }
            else
            {
                double scaleCoefficient = 1;
                if (imageInfo.Width > imageInfo.Height)
                {
                    scaleCoefficient = (double)imageInfo.Width / MaxValueInPixel;
                }
                else
                {
                    scaleCoefficient = (double)imageInfo.Height / MaxValueInPixel;
                }
                var newWidth = (int)Math.Round(imageInfo.Width / scaleCoefficient);
                var newHeight = (int)Math.Round(imageInfo.Height / scaleCoefficient);
                var result = ResizeImage(imageInfo, newWidth, newHeight);
                return result;
            }
        }

        public byte[] ImageInfoToWsq(ImageInfo imageInfo, int dpi)
        {
            var context = WsqImport.WSQCreateContext();
            int size = 0;
            var bufferOut = new byte[imageInfo.Width * imageInfo.Height];
            int error = WsqImport.RawImageToWSQ(imageInfo.Image,
                                      imageInfo.Width,
                                      imageInfo.Height,
                                      ref size,
                                      bufferOut, context);
            if (error != 0) throw new Exception("Error: " + error);
            Array.Resize(ref bufferOut, size);            
            WsqImport.WSQFreeContext(context);
            bufferOut = WsqNistToWsqTodes(bufferOut, dpi, imageInfo.Width, imageInfo.Height);
            return bufferOut;
        }

        public ImageInfo WsqToImageInfo(byte[] wsq, out int dpi)
        {
            var result = new ImageInfo();
            var context = WsqImport.WSQCreateContext();
            int width = 0, height = 0, depth = 0, ppi = 0;                      
            int error = WsqImport.WSQGetDimensions(wsq, wsq.Length, ref width, ref height, context);
            if (error != 0)
            {
                throw new Exception("Error " + error);
            }
            var bufferOut = new byte[width * height];
            error = WsqImport.WSQToRawImage(wsq, wsq.Length, ref width, ref height, ref depth, ref ppi, bufferOut, context);
            if (error == 0)
            {
                result.Width = width;
                result.Height = height;
                result.Image = bufferOut;
            }
            if (error != 0)
            {
                throw new Exception("Error " + error);
            }            
            WsqImport.WSQFreeContext(context);
            dpi = ppi;
            return result;
        }

        private byte[] WsqNistToWsqTodes(byte[] wsqNist, int dpi, int width, int height)
        {
            byte[] startInfoPattern = new byte[2] { 0xFF, 0xA0 };
            byte[] endInfoPattern = new byte[2] { 0xFF, 0xA4 };
            byte[] startArray = new byte[22] { 0xFF, 0x00, 0x02, 0x00, 0xFB, 0x00, 0x00, 0x00, 0x1E, 0x01, 
                0x00, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x5C, 0x21, 0x00, 0x00, 0xF2, 0x01};
            var startInfoPosition = PatternStartAt(wsqNist, startInfoPattern);
            var endInfoPosition = PatternStartAt(wsqNist, endInfoPattern);
            var todesWsq = new byte[wsqNist.Length - endInfoPosition + 24];
            Array.Copy(startArray, todesWsq, startArray.Length);
            Array.Copy(wsqNist, startInfoPosition, todesWsq, startArray.Length, 2);
            Array.Copy(wsqNist, endInfoPosition, todesWsq, startArray.Length + 2, wsqNist.Length - endInfoPosition);
            Array.Copy(BitConverter.GetBytes(width), 0, todesWsq, 4, 4);
            Array.Copy(BitConverter.GetBytes(height), 0, todesWsq, 8, 4);
            Array.Copy(BitConverter.GetBytes(todesWsq.Length), 0, todesWsq, 16, 4);
            Array.Copy(BitConverter.GetBytes((Int16)dpi), 0, todesWsq, 20, 2);
            return todesWsq;
        }

        private int PatternStartAt(byte[] source, byte[] pattern)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                {
                    return i;
                }
            }
            return -1;
        }
      
        public ImageInfo ResizeImage(ImageInfo imageInfo, int newWidth, int newHeight)
        {
            var bitmap = ImageInfoToBitmap(imageInfo);
            var destRect = new Rectangle(0, 0, newWidth, newHeight);
            var destImage = new Bitmap(newWidth, newHeight);
            
            destImage.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            var result = BitmapToImageInfo(destImage);
            return result;
        }
       
        public Bitmap ImageInfoToBitmap(ImageInfo imageInfo)
        {
            Bitmap outBitmap = new Bitmap(imageInfo.Width, imageInfo.Height, PixelFormat.Format8bppIndexed);
            Rectangle rectangle = new Rectangle(0, 0, outBitmap.Width, outBitmap.Height);
            BitmapData bmpData = outBitmap.LockBits(rectangle, ImageLockMode.ReadWrite, outBitmap.PixelFormat);

            if ((imageInfo.Width % 4) == 0)
            {
                bmpData.Stride = imageInfo.Width;
            }
            else
            {
                bmpData.Stride = (imageInfo.Width / 4 + 1) * 4;
            }

            bmpData.Width = imageInfo.Width;
            bmpData.Height = imageInfo.Height;

            byte[] bytesWithStride = new byte[bmpData.Stride * bmpData.Height];
            for (int i = 0; i < bmpData.Height; i++)
            {
                Array.Copy(imageInfo.Image, i * bmpData.Width, bytesWithStride, i * bmpData.Stride, bmpData.Width);
            }

            Marshal.Copy(bytesWithStride, 0, bmpData.Scan0, bytesWithStride.Length);
            outBitmap.UnlockBits(bmpData);

            ColorPalette Palette = outBitmap.Palette;
            for (int i = 0; i <= byte.MaxValue; i++)
            {
                Palette.Entries[i] = Color.FromArgb(byte.MaxValue, i, i, i);
            }
            outBitmap.Palette = Palette;
            return outBitmap;
        }

        
    }
}
