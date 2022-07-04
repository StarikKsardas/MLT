using FluentValidation;
using MLT.Web.Contracts.WebModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Web.Contracts.Validators
{
    public class UserChangePasswordValidator: AbstractValidator<UserChangePasswordWeb>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(user => user.Login).NotEmpty().WithMessage("Login or password is empty");
            RuleFor(user => user.PhoneId).NotEmpty().WithMessage("Phone Id is empty");
            RuleFor(user => user.OldPasswordEncrypt).NotEmpty().WithMessage("Login or password is empty");
            RuleFor(user => user.NewPasswordEncrypt).NotEmpty().WithMessage("Login or password is empty");
        }
    }
}
