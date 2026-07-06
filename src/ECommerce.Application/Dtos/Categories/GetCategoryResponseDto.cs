using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Dtos.Categories;

public record GetCategoryResponseDto(long Id, string Name, string Slug, string? ImageUrl) : IMapFrom<Category>;
