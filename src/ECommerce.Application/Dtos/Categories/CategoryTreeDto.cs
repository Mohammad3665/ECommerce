namespace ECommerce.Application.Dtos.Categories;

public record CategoryTreeDto(
    long Id,
    string Name,
    string Slug,
    long? ParentId,
    List<CategoryTreeDto> Children);