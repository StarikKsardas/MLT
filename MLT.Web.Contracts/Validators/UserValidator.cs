using FluentValidation;
using MLT.Web.Contracts.WebModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Web.Contracts.Validators
{
    public class UserValidator : AbstractValidator<UserWeb>
    {
        public UserValidator()
        {
            RuleFor(user => user.Login).NotEmpty().WithMessage("Login or password is empty");
            RuleFor(user => user.PhoneId).NotEmpty().WithMessage("Phone Id is empty");
            RuleFor(user => user.PasswordEncrypt).NotEmpty().WithMessage("Login or password is empty");
        }
    }
}
