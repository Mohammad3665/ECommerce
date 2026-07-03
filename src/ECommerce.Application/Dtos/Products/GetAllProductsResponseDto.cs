using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Dtos.Products;

public record GetAllProductsResponseDto(
    long Id,
    string Slug,
    string Name,
    string EnglishName,
    string ShortDescription,
    decimal Price,
    bool IsInStock,
    string BrandName,
    string CategoryName,
    string? MainImageUrl
) : IMapFrom<Product>, IHaveCustomMapping
{
    public static void ConfigureMapping(Mapster.TypeAdapterConfig config)
    {
        config.NewConfig<Product, GetAllProductsResponseDto>()
            .Map(dest => dest.MainImageUrl, src => src.Images != null 
                ? src.Images.Where(img => img.IsMain).Select(img => img.ImageUrl).FirstOrDefault() 
                : null)
                
            .Map(dest => dest.BrandName, src => src.Brand != null ? src.Brand.Name : string.Empty)
            .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty);
    }
}