using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("DACTO_DESCR_SL")]
    public class Latent
    {
        [Key]
        [Column("DS_ID")]
        public int DsId { get; set; }
        [Column("EDIT_DATE")]
        public DateTime EditDate { get; set; }
        [Column("USR_NUMBER")]
        public int UsrNumber { get; set; }
        [Column("REGNUM")]
        public string Regnum { get; set; }
        [Column("NUM_SL")]
        public byte NumSl { get; set; }
        [Column("PLACE_CODE")]
        public long PlaceCode { get; set; }
        [Column("ABR_PLACE")]
        public string AbrPlace { get; set; }
        [Column("PAPER_CRIME_NO")]
        public string PaperCrimeNo { get; set; }
        [Column("PAPER_CRIME_DATE")]
        public DateTime? PaperCrimeDate { get; set; }
        [Column("CRIME_PLACE")]
        public string CrimePlace { get; set; }
        [Column("CRIME_DATE")]
        public DateTime? CrimeDate { get; set; }
        [Column("DECISION_NO")]
        public string DecisionNo { get; set; }
        [Column("DECISION_DATE")]
        public DateTime? DecisionDate { get; set; }
        [Column("DECISION_EXPERT")]
        public string DecisionExpert { get; set; }
        [Column("UNINVOLVED_LASTNAME")]
        public string UninvolvedLastname { get; set; }
        [Column("TAKE_PLACE")]
        public string TakePlace { get; set; }
        [Column("TAKE_TYPE")]
        public string TakeType { get; set; }
        [Column("WHO_TAKE")]
        public string WhoTake { get; set; }
        [Column("INJURED")]
        public string Injured { get; set; }
        [Column("TEXT")]
        public string Text { get; set; }
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
        [Column("MASK")]
        public byte[] Mask { get; set; }
        [Column("DELTA")]
        public byte[] Delta { get; set; }
        [Column("OBJECT_FLAG")]
        public int ObjectFlag { get; set; }
        [Column("INTERPOL_TCN")]
        public string InterpolTcn { get; set; }
        [Column("INTERPOL_ORI")]
        public string InterpolOri { get; set; }
        [Column("USERCHANGE_DATE")]
        public DateTime UserchangeDate { get; set; }
        [Column("SYS_TXT")]
        public string SysTxt { get; set; }
        [Column("ARC_CARD")]
        public byte ArcCard { get; set; }
        [Column("CODE_EXPERT")]
        public string CodeExpert { get; set; }
        

        public virtual ICollection<LatentImage> LatentImages { get; set; }
        public virtual ICollection<LatentEntrancePlace> LatentEntrancePlaces { get; set; }
        public virtual ICollection<LatentEntranceType> LatentEntranceTypes { get; set; }
        public virtual ICollection<LatentCrimeType> LatentCrimeTypes { get; set; }
        public virtual ICollection<AnswerMobile> AnswerMobiles { get; set; }
    }
}
