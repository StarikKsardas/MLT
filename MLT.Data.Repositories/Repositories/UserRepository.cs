using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Repositories;
using MLT.Data.Repositories.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace MLT.Data.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MLTDbContext mLTDbContext;
        public UserRepository(MLTDbContext mLTDbContext)
        {
            this.mLTDbContext = mLTDbContext;
        }

        public void Add(User user)
        {
            mLTDbContext.Users.Add(user);
            mLTDbContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            var result = mLTDbContext.Users.ToList();
            return result;
        }

        public User GetById(int id)
        {
            var result = mLTDbContext.Users.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public User GetByPhoneIdAndLogin(string phoneId, string login)
        {
            var result = mLTDbContext.Users.FirstOrDefault(x => x.PhoneId == phoneId && x.Login == login);
            return result;
        }

        public IEnumerable<User> GetByPhone(string phone)
        {
            var result = mLTDbContext.Users.Where(x => x.Phone == phone)?.ToList();
            return result;
        }

        public IEnumerable<User> GetByPhoneId(string phoneId)
        {
            var result = mLTDbContext.Users.Where(x => x.PhoneId == phoneId)?.ToList();
            return result;
        }

        public bool IsExist(string login, string phoneId, string password)
        {
            var result = false;
            var user = mLTDbContext.Users.FirstOrDefault(x => x.Login == login && x.PhoneId == phoneId && x.Password == password);
            if (user != null)
            {
                result = true;   
            }
            return result;
        }

        public int GetIdByLoginAndPassword(string login, string password)
        {
            var result = 0;
            var user = mLTDbContext.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                result = user.Id;
            }
            return result;
        }

        public bool IsExist(string login, string phoneId)
        {
            var result = false;
            var user = mLTDbContext.Users.FirstOrDefault(x => x.Login == login && x.PhoneId == phoneId);
            if (user != null)
            {
                result = true;
            }
            return result;
        }

        public void RemoveById(int id)
        {
            var user = mLTDbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                mLTDbContext.Users.Remove(user);
                mLTDbContext.SaveChanges();
            }
        }

        public void UpdateFull(User user, bool isPasswordChanged)
        {
            if (!isPasswordChanged)
            {
                var currentUser = GetById(user.Id);
                user.Password = currentUser.Password;
            }
            mLTDbContext.Entry(mLTDbContext.Users.FirstOrDefault(x => x.Id == user.Id)).CurrentValues.SetValues(user);
            mLTDbContext.SaveChanges();
        }

        public void UpdatePassword(User user)
        {
            var currentUser = GetByPhoneIdAndLogin(user.PhoneId, user.Login);
            currentUser.Password = user.Password;
            mLTDbContext.Entry(mLTDbContext.Users.FirstOrDefault(x => x.Id == currentUser.Id)).CurrentValues.SetValues(currentUser);
            mLTDbContext.SaveChanges();
        }
    }
}
