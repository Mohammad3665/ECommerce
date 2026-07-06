namespace ECommerce.Application.Features.Slides.Commands.DeleteSlide;

public class DeleteSlideCommandValidator : AbstractValidator<DeleteSlideCommand>
{
    public DeleteSlideCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithName("آیدی");
    }
}
