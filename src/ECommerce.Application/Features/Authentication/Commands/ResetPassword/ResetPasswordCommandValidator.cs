using FluentValidation;

namespace ECommerce.Application.Features.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithName("ایمیل")
            .EmailAddress();

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithName("رمز عبور فعلی")
            .MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف بزرگ باشد.")
            .Matches(@"[a-z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف کوچک باشد.")
            .Matches(@"[0-9]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک عدد باشد.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithName("رمز عبور جدید")
            .MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف بزرگ باشد.")
            .Matches(@"[a-z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف کوچک باشد.")
            .Matches(@"[0-9]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک عدد باشد.");
    }
}
