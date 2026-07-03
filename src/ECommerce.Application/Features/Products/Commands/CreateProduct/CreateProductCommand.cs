using ECommerce.Application.Common.Mapping;
using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using MediatR;

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