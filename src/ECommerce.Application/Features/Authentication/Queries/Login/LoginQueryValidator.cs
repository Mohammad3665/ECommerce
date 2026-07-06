namespace ECommerce.Application.Features.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithName("ایمیل")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithName("رمز عبور")
            .MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف بزرگ باشد.")
            .Matches(@"[a-z]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک حرف کوچک باشد.")
            .Matches(@"[0-9]").WithMessage("فیلد '{PropertyName}' باید حداقل شامل یک عدد باشد.");
    }
}
