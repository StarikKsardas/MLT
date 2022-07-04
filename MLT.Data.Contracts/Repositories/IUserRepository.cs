using MLT.Data.Contracts.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Data.Contracts.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        IEnumerable<User> GetByPhone(string phone);
        IEnumerable<User> GetByPhoneId(string phoneId);
        void RemoveById(int id);
        bool IsExist(string login, string phoneId, string password);
        bool IsExist(string login, string phoneId);
        void UpdateFull(User user, bool isPasswordChanged);
        void UpdatePassword(User user);
        int GetIdByLoginAndPassword(string login, string password);
        User GetByPhoneIdAndLogin(string phoneId, string login);
        
    }
}
