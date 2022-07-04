using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Contracts.InfoModels
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string PlaceCode { get; set; }
        public string PlaceCodeLex { get; set; }
        public string AbrPlace { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneId { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
