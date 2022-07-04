using MLT.Mobile.Droid.Services;
using MLT.Mobile.ServiceInterfaces;
using System;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(ProtectionService))]
namespace MLT.Mobile.Droid.Services
{
    public class ProtectionService : IProtectionService
    {
        public string EncryptPassword(string password)
        {
            var data = Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            var hash = Convert.ToBase64String(data);
            return hash;
        }
    }
}