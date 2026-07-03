namespace ECommerce.Application.Dtos.Products;

public record LowStockProductDto(
    long Id,
    string Name,
    string Slug,
    string SKU,
    int StockQuantity,
    decimal Price
);
