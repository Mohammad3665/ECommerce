using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Application.Article;

/// <summary>
///     Represents a blog post, news article, or content page in the content management system.
/// </summary>
public class Article : BaseEntity<long>
{
    #region Basic Content

    /// <summary>
    ///     Gets or sets the main headline or title of the article.
    /// </summary>
    /// <value>
    ///     A string containing the article's title. Should be descriptive and engaging.
    /// </value>
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the URL-friendly identifier for the article.
    /// </summary>
    /// <value>
    ///     A SEO-friendly string derived from <see cref="Title"/> (e.g., "10-essential-tips-for-learning-csharp-2024").
    /// </value>
    [Required]
    [MaxLength(1000)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the main body or full text of the article.
    /// </summary>
    /// <value>
    ///     A string containing the complete article content. May support HTML/Markdown formatting.
    /// </value>
    [Required]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the brief excerpt or summary of the article.
    /// </summary>
    /// <value>
    ///     A short string (typically 150-300 characters) summarizing the article.
    /// </value>
    [Required]
    [MaxLength(300)]
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the URL of the featured image or thumbnail for the article.
    /// </summary>
    /// <value>
    ///     A string representing the path to the article's cover image.
    /// </value>
    [Required]
    [MaxLength(300)]
    public string ImageUrl { get; set; } = string.Empty;

    #endregion

    #region Metrics & Statistics

    /// <summary>
    ///     Gets or sets the number of times this article has been viewed by users.
    /// </summary>
    /// <value>
    ///     An integer counter tracking article popularity. Defaults to 0.
    /// </value>
    public int ViewCount { get; set; }

    /// <summary>
    ///     Gets or sets the current publication state of the article.
    /// </summary>
    /// <value>
    ///     An <see cref="ArticleStatus"/> enum value (Draft, Published, or Archived).
    /// </value>
    public ArticleStatus Status { get; set; }

    #endregion

    #region Foreign Keys

    /// <summary>
    ///     Gets or sets the identifier of the category this article belongs to.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="ArticleCategory.Id"/>.
    /// </value>
    [ForeignKey(nameof(ArticleCategory))]
    public long ArticleCategoryId { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the user who wrote this article.
    /// </summary>
    /// <value>
    ///     A <see cref="Guid"/> value referencing <see cref="User.Id"/>.
    /// </value>
    [ForeignKey(nameof(User))]
    public Guid AuthorId { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the category associated with this article.
    /// </summary>
    /// <value>
    ///     An <see cref="ArticleCategory"/> entity representing the article's topic classification.
    /// </value>
    public ArticleCategory Category { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the user who wrote this article.
    /// </summary>
    /// <value>
    ///     A <see cref="User"/> entity representing the content author.
    /// </value>
    public User Author { get; set; } = null!;

    #endregion
}