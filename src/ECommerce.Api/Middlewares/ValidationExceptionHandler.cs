using Microsoft.AspNetCore.Diagnostics;
using ValidationException = FluentValidation.ValidationException;

namespace ECommerce.Api.Middlewares;

/// <summary>
/// A global exception handler that intercepts <see cref="ValidationException"/> exceptions
/// and transforms them into an HTTP 400 Bad Request response with detailed validation errors.
/// </summary>
public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Attempts to handle the given exception as a validation error.
    /// If the exception is a <see cref="ValidationException"/>, logs the event, sets a 400 status code,
    /// and writes a structured <see cref="ApiProblemDetails"/> response containing the individual validation failures.
    /// Returns <c>false</c> for any other exception type, allowing other handlers to process it.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="exception">The exception thrown during request processing.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns><c>true</c> if the exception was handled; otherwise <c>false</c>.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException ex)
            return false;

        logger.LogWarning(ex, "Validation error occurred");

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(
            new ApiProblemDetails
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status400BadRequest,
                Detail = "One or more validation errors occurred",
                TraceId = httpContext.TraceIdentifier,
                Errors = ex.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
            },
            cancellationToken);

        return true;
    }
}