using System.Diagnostics.CodeAnalysis;
using ECommerce.Domain.Common.Enums;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result.Base;

namespace ECommerce.Domain.Common.Result;

#region Result (Without data)
/// <summary>
/// Represents the outcome of an operation that either succeeds or fails, without a return value.
/// Provides an implicit conversion from <see cref="Error"/> to allow direct error propagation.
/// </summary>
public class Result : BaseResult
{
    private Result(bool isSuccess, Error.Error error)
        : base(isSuccess, error) { }

    /// <summary>
    /// Creates a successful result indicating that the operation completed without errors.
    /// </summary>
    /// <returns>A new <see cref="Result"/> representing success.</returns>
    public static Result Success() => new(true, Common.Error.Error.None);

    /// <summary>
    /// Creates a failure result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure. Must not be <see cref="Common.Error.Error.None"/>.</param>
    /// <returns>A new <see cref="Result"/> representing failure.</returns>
    public static Result Failure(Error.Error error) => new(false, error);

    /// <summary>
    /// Enables implicit conversion of an <see cref="Error"/> to a failure result,
    /// allowing a method to return an error directly where a <see cref="Result"/> is expected.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result(Error.Error error) => Failure(error);
}
#endregion

#region Result (With data)
/// <summary>
/// Represents the outcome of an operation that either succeeds and returns data,
/// or fails with an error. Implicit conversions from both the success data and an error are provided.
/// </summary>
/// <typeparam name="TData">The type of data returned on success.</typeparam>
public class Result<TData> : BaseResult
{
    private Result(TData? data, bool isSuccess, Error.Error error) : base(isSuccess, error)
    {
        Data = data;
        IsSuccess = isSuccess;
    }

    /// <summary>
    /// Gets the data produced by a successful operation.
    /// When <see cref="IsSuccess"/> is <c>true</c>, this value is guaranteed non‑null (enforced by
    /// <see cref="System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute"/>).
    /// </summary>
    public TData? Data { get; }

    /// <summary>
    /// Indicates whether the operation succeeded. Overrides the base property to carry the
    /// <see cref="MemberNotNullWhenAttribute"/> that makes <see cref="Data"/> non‑null after a <c>true</c> check.
    /// </summary>
    [MemberNotNullWhen(true, nameof(Data))]
    public override bool IsSuccess { get; }

    /// <summary>
    /// Creates a successful result containing the provided data.
    /// </summary>
    /// <param name="data">The data to return with the success result.</param>
    /// <returns>A new <see cref="Result{TData}"/> instance representing success.</returns>
    public static Result<TData> Success(TData data) => new(data, true, Common.Error.Error.None);

    /// <summary>
    /// Creates a failure result with the specified error.
    /// </summary>
    /// <param name="error">The error describing the failure. Must not be <see cref="Common.Error.Error.None"/>.</param>
    /// <returns>A new <see cref="Result{TData}"/> instance representing failure.</returns>
    public static Result<TData> Failure(Error.Error error) => new(default, false, error);

    /// <summary>
    /// Enables implicit conversion of a data value to a successful result,
    /// allowing a method to return a value directly where a <see cref="Result{TData}"/> is expected.
    /// </summary>
    /// <param name="data">The data to convert.</param>
    public static implicit operator Result<TData>(TData data) => Success(data);

    /// <summary>
    /// Enables implicit conversion of an <see cref="Error"/> to a failure result,
    /// allowing a method to return an error directly where a <see cref="Result{TData}"/> is expected.
    /// </summary>
    /// <param name="error">The error to convert.</param>
    public static implicit operator Result<TData>(Error.Error error) => Failure(error);
}
#endregion

#region Query result

/// <summary>
/// Represents the complete result of a query including data, pagination metadata, sorting information, and applied filters.
/// This class provides a comprehensive response for data retrieval operations with filtering, sorting, and pagination.
/// </summary>
/// <typeparam name="T">The type of items in the data collection.</typeparam>
/// <param name="items">The paginated collection of items.</param>
/// <param name="currentPage">The current page number (1-based index).</param>
/// <param name="pageSize">The number of items per page.</param>
/// <param name="totalCount">The total number of items across all pages.</param>
/// <param name="sortBy">The property name used for sorting, or null if no sorting was applied.</param>
/// <param name="sort">The direction of sorting applied to the results.</param>
/// <param name="appliedFilters">The collection of filter conditions that were applied to the query.</param>
public class QueryResult<T>(
    IReadOnlyList<T> items,
    int currentPage,
    int pageSize,
    int totalCount,
    string? sortBy,
    SortType sort,
    IReadOnlyList<FilterCondition>? appliedFilters
)
{
    /// <summary>
    /// Gets the collection of items for the current page.
    /// </summary>
    public IReadOnlyList<T> Items { get; } = items ?? throw new ArgumentNullException(nameof(items));

    /// <summary>
    /// Gets the current page number (1-based index).
    /// </summary>
    public int CurrentPage { get; } = currentPage;

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    public int PageSize { get; } = pageSize;

    /// <summary>
    /// Gets the total number of items across all pages.
    /// </summary>
    public int TotalCount { get; } = totalCount;

    /// <summary>
    /// Gets the total number of pages available.
    /// Calculated as ceiling(TotalCount / PageSize).
    /// </summary>
    public int TotalPages { get; } = (int)Math.Ceiling(totalCount / (double)pageSize);

    /// <summary>
    /// Gets the property name used for sorting the results.
    /// Returns null if no sorting was applied.
    /// </summary>
    public string? SortBy { get; } = sortBy;

    /// <summary>
    /// Gets the sort direction applied to the results.
    /// </summary>
    public SortType Sort { get; } = sort;

    /// <summary>
    /// Gets the collection of filter conditions that were applied to generate these results.
    /// Returns null if no filters were applied.
    /// </summary>
    public IReadOnlyList<FilterCondition>? AppliedFilters { get; } = appliedFilters;

    /// <summary>
    /// Gets a value indicating whether there is a next page available.
    /// Returns true if CurrentPage is less than TotalPages.
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;

    /// <summary>
    /// Gets a value indicating whether there is a previous page available.
    /// Returns true if CurrentPage is greater than 1.
    /// </summary>
    public bool HasPrev => CurrentPage > 1;

    /// <summary>
    /// Transforms the collection items of the current query result into another type 
    /// using the specified mapper function, while preserving all pagination, sorting, and filtering metadata.
    /// </summary>
    /// <typeparam name="TTarget">The destination type to convert the items into.</typeparam>
    /// <param name="mapper">A delegate function that defines how to map each item from <typeparamref name="T"/> to <typeparamref name="TTarget"/>.</param>
    /// <returns>A new <see cref="QueryResult{TTarget}"/> instance containing the transformed items and the original query metadata.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="mapper"/> function is null.</exception>
    public QueryResult<TTarget> Map<TTarget>(Func<T, TTarget> mapper)
    {
        if (mapper == null) throw new ArgumentNullException(nameof(mapper));
        var mappedItems = Items.Select(mapper).ToList();

        return new QueryResult<TTarget>(
            mappedItems,
            CurrentPage,
            PageSize,
            TotalCount,
            SortBy,
            Sort,
            AppliedFilters
        );
    }
}

#endregion