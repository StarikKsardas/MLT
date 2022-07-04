using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Web.Contracts.WebModels
{
    public class UserChangePasswordWeb
    {
        public string Login { get; set; }
        public string OldPasswordEncrypt { get; set; }
        public string NewPasswordEncrypt { get; set; }
        public string PhoneId { get; set; }
    }
}
