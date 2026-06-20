using FluentValidation;

namespace ECommerce.Application.Features.Users.Commands.EditUserProfile;

public class EditUserProfileCommandValidator : AbstractValidator<EditUserProfileCommand>
{
    public EditUserProfileCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithName("نام و نام‌خانوادگی")
            .Must(name => !name.Any(char.IsDigit))
            .WithMessage("فیلد '{PropertyName}' نباید شامل اعداد باشد.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithName("شماره‌تماس")
            .MaximumLength(11)
            .Matches(@"[0-9]")
            .WithMessage("فیلد '{PropertyName}' نباید شامل حروف باشد.");
    }
}
