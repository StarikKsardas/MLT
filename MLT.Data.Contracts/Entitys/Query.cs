using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_QUERY")]
    public class Query
    {   
        [Key]
        [Column("QUERY_ID")]
        public int QueryId { get; set; }
        [Column("S_DS_ID")]
        public int SDsId { get; set; }
        [Column("S_USR_ID")]
        public string SUsrId { get; set; }
        [Column("S_PLACE_CODE")]
        public long SPlaceCode { get; set; }
        [Column("S_ABR_PLACE")]
        public string SAbrPlace { get; set; }
        [Column("S_EDIT_DATE")]
        public DateTime SEditDate { get; set; }
        [Column("LOCAL_STATUS")]
        public int LocalStatus { get; set; }
    }
}
