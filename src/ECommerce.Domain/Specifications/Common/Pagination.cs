namespace ECommerce.Domain.Specifications.Common;

/// <summary>
/// Represents a page of items resulting from a paginated query, along with pagination metadata.
/// </summary>
/// <typeparam name="T">The type of the items in the page.</typeparam>
public class Pagination<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Pagination{T}"/> class with default values.
    /// The <see cref="Items"/> collection is empty, and <see cref="CurrentPage"/> and <see cref="TotalPages"/> are zero.
    /// </summary>
    public Pagination() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pagination{T}"/> class with the specified items,
    /// current page number, total item count, and page size.
    /// </summary>
    /// <param name="items">The items belonging to the current page.</param>
    /// <param name="currentPage">The 1-based page number of the returned items.</param>
    /// <param name="totalCount">The total number of items across all pages.</param>
    /// <param name="pageSize">The number of items per page. Used to calculate <see cref="TotalPages"/>.</param>
    public Pagination(
        IReadOnlyList<T> items,
        int currentPage,
        int totalCount,
        int pageSize
    )
    {
        Items = items;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(totalCount / (decimal)pageSize);
    }

    /// <summary>
    /// The items on the current page.
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = [];

    /// <summary>
    /// The current page number (1-based).
    /// </summary>
    public int CurrentPage { get; init; }

    /// <summary>
    /// The total number of pages, calculated from the total item count and page size.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Indicates whether a next page exists.
    /// </summary>
    public bool IsExistNextPage => (CurrentPage < TotalPages);

    /// <summary>
    /// Indicates whether a previous page exists.
    /// </summary>
    public bool IsExistPrevPage => (CurrentPage > 1);
}