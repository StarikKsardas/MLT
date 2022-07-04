using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Entitys
{
    [Table("WEB_USER")]
    public class User
    {
        [Key]
        [Column("USR_ID")]
        public int Id { get; set; }
        [Column("PLACE_CODE")]
        public long PlaceCode { get; set; }
        [Column("PLACE_CODE_LEX")]
        public string PlaceCodeLex { get; set; }
        [Column("ABR_PLACE")]
        public string AbrPlace { get; set; }
        [Column("FIRSTNAME")]
        public string FirstName { get; set; }
        [Column("LASTNAME")]
        public string LastName { get; set; }
        [Column("MIDNAME")]
        public string MidName { get; set; }
        [Column("LOGIN")]
        public string Login { get; set; }
        [Column("PASS")]
        public string Password { get; set; }
        [Column("REG_DATE")]
        public DateTime RegistrationDate { get; set; }
        [Column("PHONE_ID")]
        public string PhoneId { get; set; }
        [Column("PHONE")]
        public string Phone { get; set; }
        [Column("UPDATE_DATE")]
        public DateTime UpdateDate { get; set; }
    }
}
