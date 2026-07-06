namespace ECommerce.Application.Features.Categories.Commands.ToggleCategoryStatus;

public class ToggleCategoryStatusCommandValidator : AbstractValidator<ToggleCategoryStatusCommand>
{
    public ToggleCategoryStatusCommandValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
    }
}