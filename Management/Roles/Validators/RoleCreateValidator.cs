using FluentValidation;
using Management.Roles.Dto;

namespace Management.Roles.Validators
{
    public class RoleCreateValidator : AbstractValidator<RoleCreateDto>
    {
        public RoleCreateValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required!");
        }
    }

}
