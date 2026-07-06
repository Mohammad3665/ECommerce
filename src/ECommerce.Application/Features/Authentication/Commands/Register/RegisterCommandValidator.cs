namespace ECommerce.Application.Features.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithName("نام و نام‌خانوادگی")
            .Must(name => !name.Any(char.IsDigit))
            .WithMessage("فیلد '{PropertyName}' نباید شامل اعداد باشد.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithName("ایمیل")
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithName("شماره‌تماس")
            .MaximumLength(11)
            .Matches(@"[0-9]")
            .WithMessage("فیلد '{PropertyName}' نباید شامل حروف باشد.");

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithName("رمز عبور")
            .Matches(@"[A-Z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف بزرگ باشد.")
            .Matches(@"[a-z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف کوچک باشد.")
            .Matches(@"[0-9]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک عدد باشد.");
    }
}
