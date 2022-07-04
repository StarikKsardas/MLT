using FluentValidation;
using MLT.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Mobile.Validators
{
    public class LatentValidator : AbstractValidator<LatentMobile>
    {
        public LatentValidator()
        {
            RuleFor(p => p.RegistrationNumber).NotEmpty().WithMessage("Registration Number can't be empty");
            RuleFor(p => p.LatentNumber).InclusiveBetween(1, 99).WithMessage("Latent Number must be betwwen 1 to 99")
                .NotEmpty().WithMessage("Latent Number can't be empty");
            RuleFor(p => p.ImageBase64).NotEmpty().WithMessage("Image can't be null");
            RuleFor(p => p.CrimeDate.Date).LessThanOrEqualTo(DateTime.Now.Date).WithMessage("Date can't be in future");
        }
    }
}
