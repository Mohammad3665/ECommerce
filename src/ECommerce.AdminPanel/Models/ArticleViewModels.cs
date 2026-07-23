namespace ECommerce.AdminPanel.Models;

public class ArticleListViewModel
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string EnglishTitle { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public DateTime? PublishedAt { get; set; }
    public int Status { get; set; }
}

public class ArticleFormViewModel
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string EnglishTitle { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public long ArticleCategoryId { get; set; }
    public int Status { get; set; }
    public DateTime? PublishedAt { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? ExistingImageUrl { get; set; }
}
