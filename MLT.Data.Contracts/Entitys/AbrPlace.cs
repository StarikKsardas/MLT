using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_KL_ABR_PLACE")]
    public class AbrPlace
    {
        [Key]
        [Column("ABR_PLACE")]
        public string Place { get; set; }
    }
}
