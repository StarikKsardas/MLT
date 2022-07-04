using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Contracts.InfoModels
{
    public class ImageInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Image { get; set; }
    }
}
