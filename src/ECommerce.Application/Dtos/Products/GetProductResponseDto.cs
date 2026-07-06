using ECommerce.Domain.Entities.Product;
using Mapster;

namespace ECommerce.Application.Dtos.Products;

public record GetProductResponseDto(
    long Id,
    string Slug,
    string Name,
    string EnglishName,
    string Description,
    string ShortDescription,
    decimal Price,
    int StockQuantity,
    string BrandSlug,
    string BrandName,
    string CategorySlug,
    string CategoryName,
    List<SpecificationDto> ProductSpecifications,
    List<ProductImageDto> Images,
    DateTime CreatedAt,
    DateTime? UpdatedAt
) : IHaveCustomMapping
{
    public Dictionary<string, string> Specifications =>
        ProductSpecifications?
            .ToDictionary(s => s.Key, s => s.Value) ?? new Dictionary<string, string>();

    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Product, GetProductResponseDto>()
            .Map(dest => dest.BrandName, src => src.Brand != null ? src.Brand.Name : string.Empty)
            .Map(dest => dest.BrandSlug, src => src.Brand.Slug != null ? src.Brand.Slug : string.Empty)

            .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty)
            .Map(dest => dest.CategorySlug, src => src.Category.Slug != null ? src.Category.Slug : string.Empty)

            .Map(dest => dest.ProductSpecifications, src => src.Specifications)
            .Map(dest => dest.Images, src => src.Images);
    }
}