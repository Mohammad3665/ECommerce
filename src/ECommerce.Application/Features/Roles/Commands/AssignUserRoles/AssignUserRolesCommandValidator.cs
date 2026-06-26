using FluentValidation;

namespace ECommerce.Application.Features.Roles.Commands.AssignUserRoles;

public class AssignUserRolesCommandValidator : AbstractValidator<AssignUserRolesCommand>
{
    public AssignUserRolesCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithName("آیدی کاربر");

        RuleFor(x => x.RoleSlugs)
            .NotEmpty()
            .WithName("اسلاگ نقش‌ها");
    }
}
