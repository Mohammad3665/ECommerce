namespace ECommerce.Application.Dtos.Slides;

public record CreateSlideRequestDto(
    string Title,
    string EnglishTitle,
    string Description,
    string Link,
    int DisplayOrder,
    bool IsActive
);