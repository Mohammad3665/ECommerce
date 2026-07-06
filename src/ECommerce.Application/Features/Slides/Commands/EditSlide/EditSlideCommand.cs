namespace ECommerce.Application.Features.Slides.Commands.EditSlide;

public record EditSlideCommand(
    long Id,
    string Title,
    string EnglishTitle,
    string Description,
    string? ImageUrl,
    string Link,
    int DisplayOrder
) : IRequest<Result>;