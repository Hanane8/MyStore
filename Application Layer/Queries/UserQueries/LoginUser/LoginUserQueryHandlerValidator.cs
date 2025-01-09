using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.UserQueries.LoginUser
{
    public class LoginUserQueryHandlerValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryHandlerValidator()
        {
            RuleFor(x => x.LoginUserDTO.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.LoginUserDTO.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
}
