using AutoMapper;
using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Repositories;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Services.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IProtectionService protectionService;

        public UserService(IUserRepository userRepository, IMapper mapper, IProtectionService protectionService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.protectionService = protectionService;
        }

        public void Add(UserInfo userInfo)
        {
            var user = mapper.Map<UserInfo, User>(userInfo);
            var now = DateTime.Now;
            user.RegistrationDate = now;
            user.UpdateDate = now;
            user.Password = protectionService.EncryptPassword(userInfo.Password);
            userRepository.Add(user);
        }

        public int ConnectToDb(UserInfo userInfo)
        {            
            var password = protectionService.EncryptPassword(userInfo.Password);
            var result = userRepository.GetIdByLoginAndPassword(userInfo.Login, password);
            return result;
        }

        public IEnumerable<UserInfo> GetAllUsers()
        {
            var users = userRepository.GetAll();
            var userInfos = mapper.Map<IEnumerable<User>, IEnumerable<UserInfo>>(users);
            return userInfos;
        }

        public void Remove(int id)
        {
            userRepository.RemoveById(id);
        }

        public void Update(UserInfo userInfo, bool isPasswordChanged)
        {
            var user = mapper.Map<UserInfo, User>(userInfo);
            user.UpdateDate = DateTime.Now;
            if (isPasswordChanged)
            {
                user.Password = protectionService.EncryptPassword(userInfo.Password);
            }
            userRepository.UpdateFull(user, isPasswordChanged);
        }

        public bool ValidateExist(UserInfo userInfo)
        {
            var result = userRepository.IsExist(userInfo.Login, userInfo.PhoneId);
            return result;
        }

        public UserInfo SignIn(UserInfo userInfo)
        {
            UserInfo result = null;
            if (userRepository.IsExist(userInfo.Login, userInfo.PhoneId, userInfo.Password))
            {
                var user = userRepository.GetByPhoneIdAndLogin(userInfo.PhoneId, userInfo.Login);
                result = mapper.Map<User, UserInfo>(user);
                result.Password = "";
            }
            return result;
        }

        public bool ChangePassword(UserInfo userInfo, string newPasswordEncrypted)
        {
            if (userRepository.IsExist(userInfo.Login, userInfo.PhoneId, userInfo.Password))
            {
                var user = mapper.Map<UserInfo, User>(userInfo);
                user.UpdateDate = DateTime.Now;
                user.Password = newPasswordEncrypted;
                userRepository.UpdatePassword(user);
                return true;
            }
            else
            {
                throw new Exception("Пользователь не найден или старый пароль неверен.");
            }
        }
    }
}
