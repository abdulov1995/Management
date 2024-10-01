using FluentValidation;
using Management.Users.Dto;

namespace Management.Users.Validators
{
    public class UserUpdateValidator : AbstractValidator<UpdateUserDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Name is required!");
            RuleFor(t => t.Age).NotEmpty().WithMessage("Name is required!");
        }
    }
}
