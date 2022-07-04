using FluentValidation;
using MLT.Desktop.AppUsers.Contracts.ViewModels;

namespace MLT.Desktop.AppUsers.Contracts.Validators
{
    public class LoginValidator : AbstractValidator<LoginView>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Login).NotEmpty().WithMessage("Cann't be empty");
            RuleFor(login => login.Password).NotEmpty().WithMessage("Cann't be empty");           
        }
    }
}
