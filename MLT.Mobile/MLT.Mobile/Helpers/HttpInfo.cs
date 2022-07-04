using RestSharp;
using System;

namespace MLT.Mobile.Helpers
{
    public class HttpInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public Method MethodType { get; set; }        
        public string JsonData { get; set; }
        public string Login { get; set; }
        public string EncryptPassword { get; set; }
        public string PhoneId { get; set; }
        
    }
}
