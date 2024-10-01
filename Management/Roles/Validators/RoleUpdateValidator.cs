using FluentValidation;
using Management.Roles.Dto;

namespace Management.Roles.Validators
{
    public class RoleUpdateValidator : AbstractValidator<UpdateRoleDto>
    {
        public RoleUpdateValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Name is required!");
        }
    }

}
