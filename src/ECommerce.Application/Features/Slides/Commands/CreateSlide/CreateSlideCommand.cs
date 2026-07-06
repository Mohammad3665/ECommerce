using ECommerce.Domain.Entities.Application.Slide;

namespace ECommerce.Application.Features.Slides.Commands.CreateSlide;

public record CreateSlideCommand(
    string Title,
    string EnglishTitle,
    string Description,
    string ImageUrl,
    string Link,
    int DisplayOrder,
    bool IsActive
) : IMapTo<Slide>, IRequest<Result<long>>;
