using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("WEB_ANSWER_MOBILE")]
    public class AnswerMobile
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("DS_ID")]
        public int DsId { get; set; }
        [Column("QUERY_ID")]
        public int QueryId { get; set; }
        [Column("LOCAL_STATUS")]
        public int LocalStatus { get; set; }

        public virtual Latent Latent { get; set; }
        public virtual QueryStatus QueryStatus { get; set; }
    }
}
