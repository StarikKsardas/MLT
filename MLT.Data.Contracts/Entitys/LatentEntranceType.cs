using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_ENTRANCE_TYPE_SL")]
    public class LatentEntranceType
    {
        [Key]
        [Column("DS_ID")]
        public int DsId { get; set; }
        [Key]
        [Column("ENTRANCE_TYPE_ID")]
        public byte EntranceTypeId { get; set; }
        [Column("TEXT1")]
        public string Text1 { get; set; }
        [Column("TEXT2")]
        public string Text2 { get; set; }

        public virtual Latent Latent { get; set; }
    }
}
