using Microsoft.AspNetCore.Http;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLT.Web.Services.Rest.Middlwares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        
        public AuthorizationMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;            
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService)
        {
            var login = httpContext.Request.Headers["Login"].ToString();
            var encryptPassword = httpContext.Request.Headers["EncryptPassword"].ToString();
            var phoneId = httpContext.Request.Headers["PhoneId"].ToString();
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(phoneId))
            {
                var userInfo = new UserInfo()
                {
                    Login = login,
                    Password = encryptPassword,
                    PhoneId = phoneId
                };
                var user = userService.SignIn(userInfo);
                if (user != null)
                {
                    httpContext.Items["UserInfo"] = user;                    
                }
            }
            await requestDelegate.Invoke(httpContext);

        }
    }
}
