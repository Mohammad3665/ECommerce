using ECommerce.Domain.Common.Enums;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result.Base;

namespace ECommerce.Domain.Common.Result;

#region Result (No data)

/// <summary>
/// Represents the outcome of an operation without returning data.
/// Provides factory methods for success, failure, not found, and warning results.
/// </summary>
public class Result : BaseReault
{
    private Result(
        bool succeeded,
        ResultType type, 
        string message, 
        Exception? exception = null, 
        List<string>? errors = null) : base(succeeded, type, message, exception){}


    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <param name="message">Optional success message.</param>
    /// <returns>A success result object.</returns>
    public static Result Success(string message = "")
        => new(true, ResultType.Success, message);


    /// <summary>
    /// Creates a failed result with an error message.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="exception">Optional exception that caused the error.</param>
    /// <returns>A failure result object.</returns>
    public static Result Failure(string message, Exception? exception)
        => new(false, ResultType.Error, message, exception);


    /// <summary>
    /// Creates a failed result with validation errors.
    /// </summary>
    /// <param name="errors">List of validation errors.</param>
    /// <param name="message">Error message.</param>
    /// <returns>A failure result object.</returns>
    public static Result Failure(List<string> errors, string message = "Validation Failed.")
        => new(false, ResultType.Error, message, null, errors);


    /// <summary>
    /// Creates a "Not Found" result, typically used when data is missing.
    /// </summary>
    /// <param name="message">Optional message explaining what was not found.</param>
    /// <returns>A not found result object.</returns>
    public static Result NotFound(string message = "")
        => new(false, ResultType.NotFound, message);


    /// <summary>
    /// Creates a warning result, indicating a non-critical issue.
    /// </summary>
    /// <param name="message">Optional warning message.</param>
    /// <returns>A warning result object.</returns>
    public static Result Warning(string message = "")
        => new(true, ResultType.Warning, message);
}

#endregion

#region Result(With Data)

/// <summary>
/// Represents the outcome of an operation that returns data.
/// Provides factory methods for success, failure, not found, and warning results.
/// </summary>
/// <typeparam name="TData">Type of data returned by the operation.</typeparam>
public class Result<TData> : BaseReault
{
    /// <summary>
    /// The data returned by the operation.
    /// </summary>
    public TData Data { get; }

    private Result(
        bool succeeded,
        ResultType type,
        string message,
        TData data = default!,
        Exception? exception = null,
        List<string>? errors = null) : base(succeeded, type, message, exception, errors)
    {
        Data = data;
    }


    /// <summary>
    /// Creates a successful result with data.
    /// </summary>
    /// <param name="data">Returned data.</param>
    /// <param name="message">Optional success message.</param>
    /// <returns>A success result object containing data.</returns>
    public static Result<TData> Success(TData data, string message = "")
        => new(true, ResultType.Success, message, data);


    /// <summary>
    /// Creates a failed result with an error message.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="exception">Optional exception that caused the error.</param>
    /// <returns>A failure result object.</returns>
    public static Result<TData> Failure(string message, Exception? exception)
        => new(false, ResultType.Error, message, default!, exception);


    /// <summary>
    /// Creates a failed result with validation errors.
    /// </summary>
    /// <param name="errors">List of validation errors.</param>
    /// <param name="message">Error message.</param>
    /// <returns>A failure result object.</returns>
    public static Result<TData> Failure(List<string> errors, string message = "Validation Failed.")
        => new(false, ResultType.Error, message, default!, null, errors);


    /// <summary>
    /// Creates a "Not Found" result when expected data could not be found.
    /// </summary>
    /// <param name="message">Optional message explaining what was not found.</param>
    /// <returns>A not found result object.</returns>
    public static Result<TData> NotFound(string message = "")
        => new(false, ResultType.NotFound, message);


    /// <summary>
    /// Creates a warning result with data, used for partial successes or recoverable issues.
    /// </summary>
    /// <param name="data">Returned data.</param>
    /// <param name="message">Optional warning message.</param>
    /// <returns>A warning result object containing data.</returns>
    public static Result<TData> Warning(string message = "")
        => new(true, ResultType.Warning, message);
    
}

#endregion

#region Result (With Data And State)

/// <summary>
/// Represents the outcome of an operation that returns both data and a custom state.
/// Useful for commands or domain operations with specific result states.
/// </summary>
/// <typeparam name="TData">Type of data returned by the operation.</typeparam>
/// <typeparam name="TState">Custom enum representing domain-specific states.</typeparam>
public class Result<TData, TState> : BaseReault where TState : Enum
{
    /// <summary>
    /// The data returned by the operation.
    /// </summary>
    public TData Data { get; }

    /// <summary>
    /// Custom state representing a specific condition or reason for the result.
    /// </summary>
    public TState State { get; }

    private Result(
        bool succeeded,
        ResultType type,
        string message,
        TData data,
        TState state,
        Exception? exception = null,
        List<string>? errors = null) : base(succeeded, type, message, exception, errors)
    {
        Data = data;
        State = state;
    }


    /// <summary>
    /// Creates a successful result with data and a custom state.
    /// </summary>
    /// <param name="data">Returned data.</param>
    /// <param name="state">Custom state representing the result context.</param>
    /// <param name="message">Optional success message.</param>
    /// <returns>A success result object containing data and state.</returns>
    public static Result<TData, TState> Success(TData data, TState state, string message = "")
        => new(true, ResultType.Success, message, data, state);


    /// <summary>
    /// Creates a failed result with a custom state and message.
    /// </summary>
    /// <param name="state">Custom state representing the failure reason.</param>
    /// <param name="message">Error message.</param>
    /// <param name="exception">Optional exception that caused the error.</param>
    /// <returns>A failure result object containing state information.</returns>
    public static Result<TData, TState> Failure(TState state, string message, Exception? exception = null)
        => new(false, ResultType.Error, message, default!, state, exception);


    /// <summary>
    /// Creates a failed result with validation errors and custom state.
    /// </summary>
    /// <param name="state">Custom state representing the failure reason.</param>
    /// <param name="errors">List of validation errors.</param>
    /// <param name="message">Error message.</param>
    /// <returns>A failure result object containing state information and errors.</returns>
    public static Result<TData, TState> Failure(TState state, List<string> errors, string message = "Validation Failed.")
        => new(false, ResultType.Error, message, default!, state, null, errors);


    /// <summary>
    /// Creates a "Not Found" result with a custom state.
    /// </summary>
    /// <param name="state">Custom state representing what was not found.</param>
    /// <param name="message">Optional message explaining what was not found.</param>
    /// <returns>A not found result object containing state information.</returns>
    public static Result<TData, TState> NotFound(TState state, string message = "")
        => new(false, ResultType.NotFound, message, default!, state);

    
    /// <summary>
    /// Creates a warning result with data and a custom state.
    /// </summary>
    /// <param name="data">Returned data.</param>
    /// <param name="state">Custom state representing the warning context.</param>
    /// <param name="message">Optional warning message.</param>
    /// <returns>A warning result object containing data and state.</returns>
    public static Result<TData, TState> Warning(TData data, TState state, string message = "")
        => new(true, ResultType.Warning, message, data, state);

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
}

#endregion