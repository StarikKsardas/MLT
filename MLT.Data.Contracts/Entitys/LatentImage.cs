using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_IMAGE_SL")]
    public class LatentImage
    {
        [Key]
        [Column("DS_ID")]
        public int DsId { get; set; }
        [Key]
        [Column("IMAGE_ID")]
        public byte ImageId { get; set; }
        [Column("IMAGE")]        
        public byte[] Image { get; set; }
        [Column("CHECKSUM")]
        public int Checksum { get; set; }

        public virtual Latent Latent { get; set; }
    }
}
