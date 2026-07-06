namespace ECommerce.Application.Features.Slides.Commands.DeleteSlide;

public record DeleteSlideCommand(long Id) : IRequest<Result>;
