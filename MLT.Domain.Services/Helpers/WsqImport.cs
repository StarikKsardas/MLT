using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace MLT.Domain.Services.Helpers
{
    public static class WsqImport
    {       
        
        private const string WSQDLL = "x64_dll/AFIS.WSQ.K.dll";
        
        [DllImport(WSQDLL)]
        public static extern int RawImageToWSQ(byte[] raw, int w, int h, ref int size, byte[] bufferOut, IntPtr context);

        [DllImport(WSQDLL)]
        public static extern int WSQToRawImage(byte[] wsq, int size, ref int width, ref int height, ref int depth, ref int ppi, byte[] raw, IntPtr context);

        [DllImport(WSQDLL)]
        public static extern int WSQGetDimensions(byte[] wsq, int size, ref int width, ref int height, IntPtr context);

        [DllImport(WSQDLL)]
        public static extern IntPtr WSQCreateContext();

        [DllImport(WSQDLL)]
        public static extern int WSQFreeContext(IntPtr context);

        [DllImport(WSQDLL)]
        public static extern void FastBitmapToRaw(PixelFormat pixelformat, int width, int height, int stride, IntPtr scan,
                                                   byte[] barray);
        [DllImport(WSQDLL)]
        public static extern void FastRawToBitmap(int width, int height, int stride, IntPtr scan, byte[] barray);
    }
}
