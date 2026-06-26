using FluentValidation;

namespace ECommerce.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.PermissionIds)
            .Must(p => p == null || p.Count > 0)
            .WithMessage("در صورت ارسال لیست دسترسی‌ها، این لیست نمی‌تواند خالی باشد. حداقل یک دسترسی انتخاب کنید.");
    }
}
