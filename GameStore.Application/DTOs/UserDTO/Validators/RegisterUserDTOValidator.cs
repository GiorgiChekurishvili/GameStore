using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.UserDTO.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator()
        {
            RuleFor(p => p.UserName).MinimumLength(3).WithMessage("Username is lower than 3 characters")
                .MaximumLength(20).WithMessage("Username has over 20 characters");
            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(128).WithMessage("Password cannot exceed 128 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[!?@#$%^&+=]").WithMessage("Password must contain at least one special character.");
            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254)
            .WithMessage("Email must be a valid email address and less than 254 characters.");

        }
    }
}
