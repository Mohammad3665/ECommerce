namespace ECommerce.Application.Dtos.Products;

public record EditProductRequestDto(
    string Name,
    string EnglishName,
    string Description,
    string ShortDescription,
    decimal Price,
    int StockQuantity,
    long BrandId,
    long CategoryId,
    string? SpecificationsJson,
    string? ImagesJson
);