using Microsoft.AspNetCore.Diagnostics;

namespace ECommerce.Api.Middlewares;

/// <summary>
/// A global fallback exception handler that catches any unhandled exception,
/// logs it as an error, and returns an HTTP 500 Internal Server Error with a
/// generic problem details response.
/// </summary>
/// <param name="logger">The logger used to record unexpected errors.</param>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Handles the given exception by logging it and writing a 500‑level problem details
    /// response to the HTTP context. Always returns <c>true</c>, indicating the exception
    /// has been fully handled and no further handlers should run.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns><c>true</c>, because this handler always handles the exception.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error occurred.");

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(
            new ApiProblemDetails
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Unexpected error occurred",
                TraceId = httpContext.TraceIdentifier
            },
            cancellationToken);

        return true;
    }
}
