using ECommerce.Application.Dtos.Products;

namespace ECommerce.AdminPanel.Models;

public class ProductListViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string? MainImageUrl { get; set; }
    public bool IsInStock { get; set; }
}

public class ProductFormViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string? BrandName { get; set; }
    public string? CategoryName { get; set; }
    public long BrandId { get; set; }
    public long CategoryId { get; set; }
    public string? ExistingImageUrl { get; set; }
    public IFormFile? ImageFile { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
    public List<SpecificationDto>? Specifications { get; set; }
    public List<ProductImageDto>? Images { get; set; }
}
