using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_USER")]
    public class DactoUser
    {
        [Key]
        [Column("USR_NUMBER")]
        public int UsrNumber { get; set; }
        [Column("PLACE_CODE")]
        public long PlaceCode { get; set; }
        [Column("ABR_PLACE")]
        public string AbrPlace { get; set; }
        [Column("SUBGROUP_CODE")]
        public int SubgroupCode { get; set; }
        [Column("STATUS_CODE")]
        public int StatusCode { get; set; }
        [Column("USR_TYPE_CODE")]
        public bool UsrTypeCode { get; set; }
        [Column("USR_ID")]
        public string UsrId { get; set; }
        [Column("USR_PW")]
        public string UsrPw { get; set; }
        [Column("LASTNAME")]
        public string Lastname { get; set; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("MIDNAME")]
        public string Midname { get; set; }
        [Column("DATE_EDIT")]
        public DateTime DateEdit { get; set; }
        [Column("USR_COMMENT")]
        public string UsrComment { get; set; }
    }
}
