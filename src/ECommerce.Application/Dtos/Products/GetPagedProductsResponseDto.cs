using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Dtos.Products;

public record GetPagedProductsResponseDto(
    long Id,
    string Slug,
    string Name,
    string EnglishName,
    string ShortDescription,
    decimal Price,
    bool IsInStock,
    string BrandName,
    string CategoryName,
    string MainImageUrl
) : IMapFrom<Product>, IHaveCustomMapping
{
    public static void ConfigureMapping(Mapster.TypeAdapterConfig config)
    {
        config.NewConfig<Product, GetAllProductsResponseDto>()
            .Map(dest => dest.MainImageUrl,
            src => src.Images
                .Where(i => i.IsMain)
                .Select(i => i.ImageUrl)
                .FirstOrDefault() ??
                src.Images.Select(i => i.ImageUrl).FirstOrDefault() ??
                string.Empty)

            .Map(dest => dest.BrandName, src => src.Brand != null ? src.Brand.Name : string.Empty)
            .Map(dest => dest.CategoryName, src => src.Category != null ? src.Category.Name : string.Empty);
    }
}