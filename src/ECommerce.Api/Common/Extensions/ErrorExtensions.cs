using ECommerce.Domain.Common.Error;

namespace ECommerce.Api.Common.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Error"/> to map error types to HTTP status codes.
/// </summary>
public static class ErrorExtensions
{
    /// <summary>
    /// Maps the error’s <see cref="Error.Type"/> to a corresponding HTTP status code.
    /// </summary>
    /// <param name="error">The error to map to an HTTP status code.</param>
    /// <returns>The HTTP status code integer (e.g., 400 for validation, 404 for not found).</returns>
    public static int GetStatusCode(this Error error) => error.Type switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden => StatusCodes.Status403Forbidden,
        ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status400BadRequest
    };
}