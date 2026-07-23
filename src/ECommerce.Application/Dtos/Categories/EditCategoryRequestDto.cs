namespace ECommerce.Application.Dtos.Categories;

public record EditCategoryRequestDto(
    string Name,
    string EnglishName,
    string Description,
    long? ParentCategoryId,
    bool IsActive
);