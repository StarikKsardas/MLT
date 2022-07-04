using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using MLT.Web.Contracts.WebModels;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace MLT.Web.Services.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]   
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private IMapper mapper;
        private ILogger logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("SignIn")]
        public string SignIn([FromBody] UserWeb userWeb)
        {
            string result;
            var userInfo = mapper.Map<UserWeb, UserInfo>(userWeb);
            var resultInfo = userService.SignIn(userInfo);
            if (resultInfo != null)
            {
                result = "OK";
                logger.LogInformation($"User {userWeb.Login} connect");
            }
            else
            {
                result = "Wrong login or password";
                logger.LogInformation($"User {userWeb.Login} wrong login or password");
            }
            return result;
        }

        [HttpPost("ChangePassword")]
        public string ChangePassword([FromBody] UserChangePasswordWeb userChangePasswordWeb)
        {
            string result;
            var userInfo = mapper.Map<UserChangePasswordWeb, UserInfo>(userChangePasswordWeb);
            try
            {
                var boolResult = userService.ChangePassword(userInfo, userChangePasswordWeb.NewPasswordEncrypt);
                if (boolResult)
                {
                    result = "OK";
                    logger.LogInformation($"User {userChangePasswordWeb.Login} change password");
                }
                else
                {
                    result = "Old password is wrong";
                    logger.LogInformation($"User {userChangePasswordWeb.Login} old password is wrong");
                }
            }
            catch (Exception exception)
            {
                result = exception.Message;
                return result;
            }            
            return result;
        }
    }
}
