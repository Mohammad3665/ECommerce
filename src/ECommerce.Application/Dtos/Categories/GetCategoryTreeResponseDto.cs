namespace ECommerce.Application.Dtos.Categories;

public record CategoryTreeResponseDto(
    long Id,
    string Name,
    string Slug,
    long? ParentId,
    List<CategoryTreeResponseDto> Children);