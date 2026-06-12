namespace ECommerce.Domain.Common.Enums;

/// <summary>
/// Defines the general type or category of an operation result.
/// Used by <see cref="BaseResult"/> and derived result classes
/// to describe the overall outcome of an operation.
/// </summary>
public enum ResultType
{
    /// <summary>
    /// The operation completed successfully without any issues.
    /// </summary>
    Success = 0,

    /// <summary>
    /// The operation encountered an error that prevented completion.
    /// Typically used when an exception or validation failure occurs.
    /// </summary>
    Error = 1,

    /// <summary>
    /// The requested data or entity was not found.
    /// Commonly used when a repository or query returns no matching results.
    /// </summary>
    NotFound = 2,

    /// <summary>
    /// The operation completed but raised a non-critical warning.
    /// Often used for partial successes or recoverable states.
    /// </summary>
    Warning = 3
}