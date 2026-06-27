namespace ECommerce.Application.Dtos.Categories;

public record EditCategoryRequestDto(
    string Name,
    string EnglishName,
    string Description,
    string ImageUrl,
    long? ParentCategoryId
);
