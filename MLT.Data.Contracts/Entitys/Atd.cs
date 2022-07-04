using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_KL_PLACE")]
    public class Atd
    {
        [Key]
        [Column("PLACE_CODE")]
        public long Code { get; set; }
        [Column("LEX")]
        public string Lex { get; set; }
    }
}
