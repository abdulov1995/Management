using FluentValidation;
using Management.Auth.Dto;
using Management.Users.Dto;
using System.Text.RegularExpressions;

namespace Management.Auth.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequestDto>
    {
        public SignUpRequestValidator()
        {
            RuleFor(u => u.Email)
                .Matches(new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                .NotEmpty().NotNull()
                .WithMessage("Invalid email format.");

            RuleFor(u => u.Password)
                .Matches(new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
                .WithMessage("Invalid password format.");

        }
    }

}
