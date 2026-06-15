namespace ECommerce.Application.Dtos.Categories;

public record EditCategoryDto(
    long Id,
    string Name,
    string EnglishName,
    string Description,
    string ImageUrl,
    long? ParentCategoryId
);
