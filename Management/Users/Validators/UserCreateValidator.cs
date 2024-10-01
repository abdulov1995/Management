using FluentValidation;
using Management.Roles.Dto;
using Management.Users.Dto;

namespace Management.Users.Validators
{
    public class UserCreateValidator : AbstractValidator<CreateUserDto>
    {
        public UserCreateValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Name is required!");
            RuleFor(t => t.Age).NotEmpty().WithMessage("Name is required!");
        }
    }
}
