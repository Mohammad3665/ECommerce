using ECommerce.Domain.Entities.Application.Slide;

namespace ECommerce.Application.Dtos.Slides;

public record SlideResponseDto(
    long Id,
    string Title,
    string Description,
    string Link,
    int DisplayOrder,
    string ImageUrl
) : IMapFrom<Slide>;
