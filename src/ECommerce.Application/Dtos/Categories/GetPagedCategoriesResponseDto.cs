using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Dtos.Categories;

public class GetPagedCategoriesResponseDto : IMapFrom<Category>
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
}