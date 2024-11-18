using FluentValidation;
using Management.Auth.Dto;
using Management.Users.Dto;
using System.Text.RegularExpressions;

namespace Management.Auth.Validators
{
    public class EmailValidator : AbstractValidator<SignUpRequestDto>
    {
        public EmailValidator()
        {
            RuleFor(u => u.Email)
           .Matches(new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
           .WithMessage("Invalid email format.");
        }
    }

    public class PasswordValidator : AbstractValidator<SignUpRequestDto>
    {
        public PasswordValidator()
        {
            RuleFor(u => u.Password)
           .Matches(new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"))
           .WithMessage("Invalid password format.");
        }
    }
}
