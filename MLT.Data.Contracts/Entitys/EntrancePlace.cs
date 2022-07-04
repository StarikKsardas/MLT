using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_KL_ENTRANCE_PLACE")]
    public class EntrancePlace
    {
        [Key]
        [Column("ENTRANCE_PLACE")]
        public string EntrancePlaceLex { get; set; }
    }
}
