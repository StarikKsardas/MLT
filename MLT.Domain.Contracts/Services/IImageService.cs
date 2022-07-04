using MLT.Domain.Contracts.InfoModels;
using System.Drawing;

namespace MLT.Domain.Contracts.Services
{
    public interface IImageService
    {
        ImageInfo BitmapToImageInfo(Bitmap bitmap);
        ImageInfo NormalizeImage(ImageInfo imageInfo);
        ImageInfo Base64StringToImageInfo(string base64);
        ImageInfo WsqToImageInfo(byte[] wsq, out int dpi);
        byte[] ImageInfoToWsq(ImageInfo imageInfo, int dpi);
        Bitmap ImageInfoToBitmap(ImageInfo imageInfo);
        ImageInfo ResizeImage(ImageInfo imageInfo, int newWidth, int newHeight);
    }
}
