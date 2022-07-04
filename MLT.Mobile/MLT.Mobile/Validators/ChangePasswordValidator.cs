using FluentValidation;
using MLT.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Mobile.Validators
{
    public class ChangePasswordValidator: AbstractValidator<ChangePasswordMobile>
    {
        public ChangePasswordValidator()
        { 
            RuleFor(changePassword => changePassword.OldPassword).NotEmpty().WithMessage("Old password can't be empty")
               .MinimumLength(8).WithMessage("Old password must have minimum 8 characters");
            RuleFor(changePassword => changePassword.NewPassword).NotEmpty().WithMessage("New password can't be empty")
               .MinimumLength(8).WithMessage("New password must have minimum 8 characters")
               .NotEqual(p => p.OldPassword).WithMessage("Old and new password must be different");
            RuleFor(changePassword => changePassword.NewPassword).Equal(p => p.ConfirmedPassword).WithMessage("New and confirmed " +
                "password must be identical");
        }
    }
}
