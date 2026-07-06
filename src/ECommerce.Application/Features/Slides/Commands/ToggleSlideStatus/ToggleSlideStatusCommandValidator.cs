namespace ECommerce.Application.Features.Slides.Commands.ToggleSlideStatus;

public class ToggleSlideStatusCommandValidator : AbstractValidator<ToggleSlideStatusCommand>
{
    public ToggleSlideStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithName("آیدی");
    }
}
