using MLT.Domain.Contracts.InfoModels;
using System.Collections.Generic;

namespace MLT.Domain.Contracts.Services
{
    public interface IUserService
    {
        bool ValidateExist(UserInfo userInfo);
        void Add(UserInfo userInfo);
        void Remove(int id);
        void Update(UserInfo userInfo, bool isPasswordChanged);
        int ConnectToDb(UserInfo userInfo);
        IEnumerable<UserInfo> GetAllUsers();
        UserInfo SignIn(UserInfo userInfo);
        bool ChangePassword(UserInfo userInfo, string newPasswordEncrypted);        
    }
}
