using ECommerce.Application.Dtos.Products;

namespace ECommerce.Application.Features.Products.Commands.EditProduct;

public record EditProductCommand(
    string CurrentSlug,
    string Name,
    string EnglishName,
    string Description,
    string ShortDescription,
    decimal Price,
    int StockQuantity,
    long BrandId,
    long CategoryId,
    ICollection<SpecificationDto> Specifications,
    ICollection<ProductImageDto> Images
) : IRequest<Result>;
