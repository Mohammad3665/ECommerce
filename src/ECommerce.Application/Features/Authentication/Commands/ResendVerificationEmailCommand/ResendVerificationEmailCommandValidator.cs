namespace ECommerce.Application.Features.Authentication.Commands.ResendVerificationEmailCommand;

public class ResendVerificationEmailCommandValidator : AbstractValidator<ResendVerificationEmailCommand>
{
    public ResendVerificationEmailCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithName("ایمیل")
            .EmailAddress();
    }
}
