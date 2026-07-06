using ECommerce.Domain.Entities.Application.Slide;

namespace ECommerce.Application.Dtos.Slides;

public record AdminSlidesResponseDto(
    long Id,
    string Title,
    string EnglishTitle,
    string Description,
    string Link,
    int DisplayOrder,
    bool IsActive,
    string ImageUrl
) : IMapFrom<Slide>;