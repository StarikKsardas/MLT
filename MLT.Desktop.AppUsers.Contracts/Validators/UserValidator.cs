using FluentValidation;
using MLT.Desktop.AppUsers.Contracts.ViewModels;
using MLT.Domain.Contracts.Services;

namespace MLT.Desktop.AppUsers.Contracts.Validators
{
    public class UserValidator : AbstractValidator<UserView>
    {        
        public UserValidator()
        {            
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Lastname can't be empty");
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("Firstname can't be empty");
            RuleFor(user => user.MidName).NotEmpty().WithMessage("Midname can't be empty");
            RuleFor(user => user.Phone).NotEmpty().WithMessage("Phone can't be empty")
                .Length(13).WithMessage("Not valid phone")
                .Must(str => str != null && str.StartsWith("+375")).WithMessage("Must start from +375");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(8).WithMessage("Password must have minimum 8 characters");
            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password).WithMessage("Passwords are not equal");
            RuleFor(user => user.PlaceCode).NotEmpty().WithMessage("Place code can't be empty");
            RuleFor(user => user.Place).NotEmpty().WithMessage("Place can't be empty");
            RuleFor(user => user.PhoneId).NotEmpty().WithMessage("Phone ID can't be empty");
            RuleFor(user => user.Login).NotEmpty().WithMessage("Phone ID can't be empty")
                .MinimumLength(5).WithMessage("Login must have minimum 5 characters")
                .MaximumLength(10).WithMessage("Login must have maximum 11 characters");           
        }

    }
}
