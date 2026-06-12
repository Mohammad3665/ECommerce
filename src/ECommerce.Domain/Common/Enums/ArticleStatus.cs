namespace ECommerce.Domain.Enums;

/// <summary>
///     Represents the publication state of an article in the content management system.
/// </summary>
public enum ArticleStatus
{
    /// <summary>
    ///     Article is in progress and not yet ready for public viewing.
    /// </summary>
    Draft,

    /// <summary>
    ///     Article is publicly available and visible to all users.
    /// </summary>
    Published,

    /// <summary>
    ///     Article is removed from public view but retained for historical reference.
    /// </summary>
    Archived
}