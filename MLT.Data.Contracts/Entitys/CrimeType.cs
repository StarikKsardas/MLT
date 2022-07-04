using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_KL_CRIME_TYPE")]
    public class CrimeType
    {
        [Key]
        [Column("CRIME_TYPE")]
        public string CrimeTypeLex { get; set; }
        [Key]
        [Column("PARENT_CRIME_TYPE")]
        public string ParentCrimeTypeLex { get; set; }
        [Key]
        [Column("KLLEVEL")]
        public int IsOnlyParent { get; set; }
    }
}
