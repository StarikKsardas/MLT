using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_CRIME_TYPE_SL")]
    public class LatentCrimeType
    {
        [Key]
        [Column("DS_ID")]
        public int DsId { get; set; }
        [Key]
        [Column("CRIME_TYPE_ID")]
        public byte CrimeTypeId { get; set; }
        [Column("TEXT1")]
        public string Text1 { get; set; }
        [Column("TEXT2")]
        public string Text2 { get; set; }

        public virtual Latent Latent { get; set; }
    }
}
