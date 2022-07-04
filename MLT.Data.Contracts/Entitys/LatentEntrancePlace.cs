using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_ENTRANCE_PLACE_SL")]
    public class LatentEntrancePlace
    {
        [Key]
        [Column("DS_ID")]
        public int DsId { get; set; }
        [Key]
        [Column("ENTRANCE_PLACE_ID")]
        public byte EntrancePlaceId { get; set; }
        [Column("TEXT")]
        public string Text { get; set; }

        public virtual Latent Latent { get; set; }
    }
}
