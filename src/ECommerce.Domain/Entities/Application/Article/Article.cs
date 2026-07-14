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
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the english title of the article.
    /// </summary>
    /// <value>
    ///     A string containing the article's english title. Should be descriptive and engaging.
    /// </value>
    public string EnglishTitle { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the URL-friendly identifier for the article.
    /// </summary>
    /// <value>
    ///     A SEO-friendly string derived from <see cref="Title"/> (e.g., "10-essential-tips-for-learning-csharp-2024").
    /// </value>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the main body or full text of the article.
    /// </summary>
    /// <value>
    ///     A string containing the complete article content. May support HTML/Markdown formatting.
    /// </value>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the brief excerpt or summary of the article.
    /// </summary>
    /// <value>
    ///     A short string (typically 150-300 characters) summarizing the article.
    /// </value>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the URL of the featured image or thumbnail for the article.
    /// </summary>
    /// <value>
    ///     A string representing the path to the article's cover image.
    /// </value>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the article was published.
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the article was archived.
    /// </summary>
    public DateTime? ArchivedAt { get; set; }

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
    public long ArticleCategoryId { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the user who wrote this article.
    /// </summary>
    /// <value>
    ///     A <see cref="Guid"/> value referencing <see cref="User.Id"/>.
    /// </value>
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

    /// <summary>
    ///     Gets or sets the collection of user comments and reviews for this article.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="Comment"/> entities. Defaults to an empty list.
    /// </value>
    public ICollection<Comment> Comments { get; set; } = [];

    #endregion
}