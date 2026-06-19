using FluentValidation;

namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor (x => x.AccessToken)
            .NotEmpty()
            .WithName("توکن دسترسی");

        RuleFor (x => x.RefreshToken)
            .NotEmpty()
            .WithName("رفرش توکن");
    }
}
