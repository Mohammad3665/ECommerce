namespace ECommerce.AdminPanel.Models;

public class BrandListViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int ProductCount { get; set; }
}

public class BrandFormViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public IFormFile? LogoFile { get; set; }
    public string? ExistingLogoUrl { get; set; }
}