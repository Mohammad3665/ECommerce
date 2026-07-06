using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
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
) : IRequest<Result<long>>, IMapTo<Product>;