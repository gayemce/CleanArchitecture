using FluentValidation;

namespace CleanArchitecture.Application.Features.RoleFeatures.Command.CreateRole;
public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand> 
{
    public CreateRoleCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Rol adı boş olamaz!");
    }
}
