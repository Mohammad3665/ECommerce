using FluentValidation;

namespace ECommerce.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .Matches("^[a-zA-Z0-Empty]+$").WithMessage("نام سیستمی نقش باید فقط شامل حروف انگلیسی باشد.");
        
        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.PermissionIds)
            .NotEmpty()
            .When(x => !x.GrantAllPermissions)
            .WithMessage("انتخاب حداقل یک دسترسی برای این نقش الزامی است.");
    }
}
