using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Contracts.Services
{
    public interface IProtectionService
    {
        string EncryptPassword(string password);
    }
}
