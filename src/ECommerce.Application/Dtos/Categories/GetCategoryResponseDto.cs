using ECommerce.Domain.Entities.Product;
using Mapster;

namespace ECommerce.Application.Dtos.Categories;

public record GetCategoryResponseDto(
    long Id,
    string Name,
    string EnglishName,
    string Slug,
    string? Description,
    string? ImageUrl,
    long? ParentCategoryId,
    bool IsActive,
    int ProductCount
) : IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Category, GetCategoryResponseDto>()
            .Map(dest => dest.ProductCount, src => src.Products != null ? src.Products.Count : 0);
    }
}
