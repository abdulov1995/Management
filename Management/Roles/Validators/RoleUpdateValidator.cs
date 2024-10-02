using FluentValidation;
using Management.Roles.Dto;

namespace Management.Roles.Validators
{
    public class RoleUpdateValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required!");
        }
    }

}
