using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Domain.Entities.Product;

/// <summary>
///     Represents a user comment or review on a product, supporting nested replies/threads.
/// </summary>
public class Comment : BaseEntity<Guid>
{
    #region Basic Content

    /// <summary>
    ///     Gets or sets the title or subject line of the comment.
    /// </summary>
    /// <value>
    ///     A string containing the comment's brief headline. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the detailed body text of the comment.
    /// </summary>
    /// <value>
    ///     A string containing the full comment content. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = string.Empty;

    #endregion

    #region Foreign Keys

    /// <summary>
    ///     Gets or sets the identifier of the user who wrote this comment.
    /// </summary>
    /// <value>
    ///     A <see cref="Guid"/> value referencing <see cref="User.Id"/>.
    /// </value>
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the product this comment is about.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Product.Id"/>.
    /// </value>
    [ForeignKey(nameof(Product))]
    public long ProductId { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the parent comment for nested replies.
    /// </summary>
    /// <value>
    ///     A nullable <see cref="Guid"/> referencing the parent <see cref="Comment.Id"/>.
    ///     <c>null</c> indicates this is a top-level comment.
    /// </value>
    [ForeignKey(nameof(Comment))]
    public Guid? ParentCommentId { get; set; }

    #endregion

    #region Moderation & Status

    /// <summary>
    ///     Gets or sets a value indicating whether the comment has been approved by a moderator.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the comment is publicly visible; otherwise, <c>false</c>. Default is <c>false</c>.
    /// </value>
    public bool IsApproved { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp when the comment was approved by a moderator.
    /// </summary>
    /// <value>
    ///     A nullable <see cref="DateTime"/> representing the approval time.
    ///     <c>null</c> indicates the comment is not yet approved.
    /// </value>
    public DateTime? ApprovedAt { get; set; }

    #endregion

    #region Realtions

    /// <summary>
    ///     Gets or sets the user who authored this comment.
    /// </summary>
    /// <value>
    ///     A <see cref="User"/> entity representing the comment author.
    /// </value>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the product that this comment is associated with.
    /// </summary>
    /// <value>
    ///     A <see cref="Product"/> entity representing the reviewed product.
    /// </value>
    public Product Product { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the parent comment that this comment replies to.
    /// </summary>
    /// <value>
    ///     A <see cref="Comment"/> entity representing the parent, or <c>null</c> for top-level comments.
    /// </value>
    public Comment? ParentComment { get; set; }

    /// <summary>
    ///     Gets or sets the collection of direct replies to this comment.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="Comment"/> entities representing child replies. Defaults to an empty list.
    /// </value>
    public ICollection<Comment> Replies { get; set; } = [];

    #endregion
}