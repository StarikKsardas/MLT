using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_KL_ENTRANCE_TYPE")]
    public class EntranceType
    {
        [Key]
        [Column("ENTRANCE_TYPE")]
        public string EntranceTypeLex { get; set; }
        [Key]
        [Column("PARENT_ENTRANCE_TYPE")]
        public string ParentEntranceTypeLex { get; set; }
        [Key]
        [Column("KLLEVEL")]
        public int IsOnlyParent { get; set; }
    }
}
