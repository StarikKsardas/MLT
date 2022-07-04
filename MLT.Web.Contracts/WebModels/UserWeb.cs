using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Web.Contracts.WebModels
{
    public class UserWeb
    {
        public string Login { get; set; }
        public string PasswordEncrypt { get; set; }
        public string PhoneId { get; set; }        
    }
}
