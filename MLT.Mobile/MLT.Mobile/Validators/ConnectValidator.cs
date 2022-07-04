using FluentValidation;
using MLT.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Mobile.Validators
{
    public class ConnectValidator: AbstractValidator<ConnectMoblie>
    {
        public ConnectValidator()
        {
            RuleFor(connect => connect.Host).NotEmpty().WithMessage("Host can't be empty");
            RuleFor(connect => connect.Login).NotEmpty().WithMessage("Phone ID can't be empty")
               .MinimumLength(5).WithMessage("Login must have minimum 5 characters");
            RuleFor(connect => connect.Password).NotEmpty().WithMessage("Password can't be empty")
               .MinimumLength(8).WithMessage("Password must have minimum 8 characters");
            RuleFor(connect => connect.PhoneId).NotEmpty().WithMessage("Change phone Id in app forbidden");
            RuleFor(connect => connect.Port).NotEmpty().WithMessage("Port can't be empty. Default port is 8080");
        }
    }
}
