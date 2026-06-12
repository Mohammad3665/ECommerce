using ECommerce.Domain.Common.Enums;

namespace ECommerce.Domain.Common.Result.Base;

/// <summary>
/// Represents the base structure for all operation results.
/// Stores shared information about success, message, result type, and exceptions.
/// </summary>
/// <remarks>
/// This class is abstract and should not be instantiated directly.  
/// Use <see cref="Result{TData}"/>, <see cref="Result{TData}"/>,  
/// or <see cref="Result{TData}"/> instead.
/// </remarks>
public abstract class BaseReault
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseResult"/> class.
    /// </summary>
    /// <param name="succeeded">Indicates whether the operation succeeded.</param>
    /// <param name="type">The general result type (Success, Error, NotFound, etc.).</param>
    /// <param name="message">A descriptive message about the result.</param>
    /// <param name="exception">Optional exception associated with the result.</param>
    /// <param name="errors">List of validation errors.</param>
    protected BaseReault(bool succeeded, ResultType type, string message, Exception? exception = null, List<string>? errors = null)
    {
        Succeeded = succeeded;
        Type = type;
        Message = message;
        Exception = exception;
        Errors = errors;
    }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool Succeeded { get; }

    /// <summary>
    /// Gets the type of result (Success, Error, NotFound, Warning, etc.).
    /// </summary>
    public ResultType Type { get; }

    /// <summary>
    /// Gets a descriptive message about the result.
    /// </summary>
    /// <example>
    /// "User created successfully."  
    /// or  
    /// "Unable to connect to database."
    /// </example>
    public string Message { get; }

    /// <summary>
    /// Gets the exception that caused the operation to fail, if any.
    /// </summary>
    /// <remarks>
    /// This is typically null for successful or warning results.
    /// </remarks>
    public Exception? Exception { get; }

    /// <summary>
    /// Gets the list of validation errors. Only populated for validation failures.
    /// </summary>
    public List<string>? Errors { get; } = null!;
}