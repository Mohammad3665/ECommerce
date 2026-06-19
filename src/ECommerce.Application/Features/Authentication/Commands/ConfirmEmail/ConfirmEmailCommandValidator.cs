using FluentValidation;

namespace ECommerce.Application.Features.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithName("ایمیل")
            .EmailAddress();

        RuleFor(x => x.SecurityCode)
            .NotEmpty()
            .WithName("کد امنیتی")
            .Matches(@"[0-9]").WithMessage("فیلد '{PropertyName}' نباید شامل حروف باشد.")
            .MaximumLength(6);
    }
}
