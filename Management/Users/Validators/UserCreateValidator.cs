using FluentValidation;
using Management.Roles.Dto;
using Management.Users.Dto;

namespace Management.Users.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Name is required!");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email is required!");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is required!");
        }
    }
}
