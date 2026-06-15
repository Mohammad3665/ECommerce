namespace ECommerce.Application.Dtos.Categories;

public record CreateCategoryDto(
    string Name,
    string EnglishName,
    string? Description,
    string? ImageUrl,
    long? ParentCategoryId);
