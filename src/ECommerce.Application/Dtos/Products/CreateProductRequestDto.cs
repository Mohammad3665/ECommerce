using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Dtos.Products;

public record CreateProductRequestDto(
    string Name,
    string EnglishName,
    string Description,
    string ShortDescription,
    decimal Price,
    int StockQuantity,
    long BrandId,
    long CategoryId,
    List<SpecificationDto>? Specifications
);
