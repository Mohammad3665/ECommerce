namespace ECommerce.Domain.Common.Error;

/// <summary>
/// Categorizes application errors by their general cause, enabling fine-grained handling and client messaging.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// No error. Used as a default or sentinel value.
    /// </summary>
    None,

    /// <summary>
    /// The request contains invalid input or fails business-rule validation.
    /// </summary>
    Validation,

    /// <summary>
    /// The requested resource was not found.
    /// </summary>
    NotFound,

    /// <summary>
    /// The request conflicts with the current state of the resource (e.g., optimistic concurrency failure).
    /// </summary>
    Conflict,

    /// <summary>
    /// Authentication is required and was not provided or is invalid.
    /// </summary>
    Unauthorized,

    /// <summary>
    /// The authenticated user does not have permission to access the requested resource.
    /// </summary>
    Forbidden,

    /// <summary>
    /// Operation canceled by the user.
    /// </summary>
    Canceled,

    /// <summary>
    /// An unexpected internal error occurred, typically indicating a bug or infrastructure issue.
    /// </summary>
    Unexpected,
}