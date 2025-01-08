using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.UserCommands.RegisterUser
{
    public class RegisterUserCommandValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.NewUser.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.NewUser.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters")
                .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters");

            RuleFor(x => x.NewUser.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters")
                .Matches("^[a-zA-Z]+$").WithMessage("Last name can only contain letters");

            RuleFor(x => x.NewUser.Phone).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.NewUser.Address).NotEmpty().WithMessage("Address is required");
        }
    }
}
