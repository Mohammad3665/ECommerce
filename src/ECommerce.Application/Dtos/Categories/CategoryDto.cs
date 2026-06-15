using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Dtos.Categories;

public record CategoryDto(long Id, string Name, string Slug, string? ImageUrl) : IMapFrom<Category>;
