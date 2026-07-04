namespace ECommerce.Application.Dtos.Categories;

public record CreateCategoryRequestDto(
    string Name,
    string EnglishName,
    string? Description,
    long? ParentCategoryId);
