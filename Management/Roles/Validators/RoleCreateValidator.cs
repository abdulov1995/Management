using FluentValidation;
using Management.Roles.Dto;

namespace Management.Roles.Validators
{
    public class RoleCreateValidator : AbstractValidator<CreateRoleDto>
    {
        public RoleCreateValidator()
        {
            RuleFor(t => t.Name).NotEmpty().WithMessage("Name is required!");
        }
    }

}
