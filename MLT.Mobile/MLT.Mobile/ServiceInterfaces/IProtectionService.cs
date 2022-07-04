using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Mobile.ServiceInterfaces
{
    public interface IProtectionService
    {
        string EncryptPassword(string password);
    }
}
