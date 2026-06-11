namespace ECommerce.Domain.Entities.Application.Article;

/// <summary>
///     Represents a category or topic classification for organizing articles in the content management system.
/// </summary>
public class ArticleCategory : BaseEntity<long>
{
    #region Basic Information

    /// <summary>
    ///     Gets or sets the display name of the article category.
    /// </summary>
    /// <value>
    ///     A string containing the category's title as shown to users. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the URL-friendly identifier for the category.
    /// </summary>
    /// <value>
    ///     A SEO-friendly string derived from <see cref="Name"/> (e.g., "technology", "health-wellness").
    /// </value>
    [Required]
    [MaxLength(1000)]
    public string Slug { get; set; } = string.Empty;

    #endregion

    #region Realtions

    /// <summary>
    ///     Gets or sets the collection of articles belonging to this category.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="Article"/> entities associated with this category. Defaults to an empty list.
    /// </value>
    public ICollection<Article> Articles { get; set; } = [];

    #endregion
}
