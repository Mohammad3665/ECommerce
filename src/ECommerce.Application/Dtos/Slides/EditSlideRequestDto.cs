namespace ECommerce.Application.Dtos.Slides;

public record EditSlideRequestDto(
    string Title,
    string EnglishTitle,
    string Description,
    string Link,
    int DisplayOrder
);
