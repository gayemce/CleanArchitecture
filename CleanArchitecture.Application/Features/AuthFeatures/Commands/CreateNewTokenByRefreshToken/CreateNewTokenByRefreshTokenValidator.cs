using FluentValidation;

namespace CleanArchitecture.Application.Features.AuthFeatures.Commands.CreateNewTokenByRefreshToken;
public sealed class CreateNewTokenByRefreshTokenValidator : AbstractValidator<CreateNewTokenByRefreshTokenCommand>
{
    public CreateNewTokenByRefreshTokenValidator()
    {
        RuleFor(p => p.UserId).NotEmpty().WithMessage("User bilgisi boş olamaz!");
        RuleFor(p => p.RefreshToken).NotEmpty().WithMessage("Refresh Token bilgisi boş olamaz!");
    }
}
