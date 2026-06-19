using System.Data;
using FluentValidation;

namespace ECommerce.Application.Features.Authentication.Commands.ResetForgotedPassword;

public class ResetForgotedPasswordCommandValidator : AbstractValidator<ResetForgotedPasswordCommand>
{
    public ResetForgotedPasswordCommandValidator()
    {
        RuleFor (x => x.Email)
            .NotEmpty()
            .WithName("ایمیل")
            .EmailAddress();
        
        RuleFor (x => x.SecurityCode)
            .NotEmpty()
            .WithName("کد امنیتی")
            .Matches(@"[0-9]").WithMessage("فیلد '{PropertyName}' نباید شامل حروف باشد.")
            .MaximumLength(6);
        
        RuleFor (x => x.NewPassword)
            .NotEmpty()
            .WithName("رمز عبور جدید")
            .MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف بزرگ باشد.")
            .Matches(@"[a-z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف کوچک باشد.")
            .Matches(@"[0-9]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک عدد باشد.");
    }
}
