namespace ECommerce.AdminPanel.Models;

public class ArticleCategoryListViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public int ArticleCount { get; set; }
}

public class ArticleCategoryFormViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EnglishName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}
