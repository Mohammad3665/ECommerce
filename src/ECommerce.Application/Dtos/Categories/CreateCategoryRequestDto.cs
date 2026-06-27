namespace ECommerce.Application.Dtos.Categories;

public record CreateCategoryRequestDto(
    string Name,
    string EnglishName,
    string? Description,
    string? ImageUrl,
    long? ParentCategoryId);
