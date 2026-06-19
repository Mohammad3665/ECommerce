using FluentValidation;

namespace ECommerce.Application.Features.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor (x => x.Email)
            .NotEmpty()
            .WithName("ایمیل")
            .EmailAddress();
    }
}
