using MLT.Domain.Contracts.Services;
using System;
using System.Text;

namespace MLT.Domain.Services.Services
{
    public class ProtectionService: IProtectionService
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
