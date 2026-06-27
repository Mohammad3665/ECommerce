using FluentValidation;

namespace ECommerce.Application.Features.Brands.Commands.ToggleBrandStatus;

public class ToggleBrandStatusCommandValidator : AbstractValidator<ToggleBrandStatusCommand>
{
    public ToggleBrandStatusCommandValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
    }
}
