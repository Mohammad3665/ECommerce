using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Dtos.Categories;

public record GetPagedCategoriesResponseDto(long Id, string Name, string EnglishName, string Slug, string? ImageUrl)
    : IMapFrom<Category>;
