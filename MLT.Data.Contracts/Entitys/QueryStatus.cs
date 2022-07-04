using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_KL_QUERY_STATUS")]
    public class QueryStatus
    {
        [Key]
        [Column("LOCAL_STATUS")]
        public int LocalStatus { get; set; }
        [Column("LEX")]
        public string StatusLex { get; set; }

        public virtual ICollection<AnswerMobile> AnswerMobile {get; set;}
    }
}
