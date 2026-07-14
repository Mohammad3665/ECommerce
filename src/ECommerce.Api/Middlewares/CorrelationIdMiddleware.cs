using Serilog.Context;

namespace ECommerce.Api.Middlewares;

/// <summary>
///     Middleware for managing correlation IDs across HTTP requests and logging.
/// </summary>
/// <remarks>
///     <para>
///         A correlation ID is a unique identifier that is assigned to each HTTP request
///         and propagated through all layers of the application. It enables:
///     </para>
/// </remarks>
public class CorrelationIdMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     The HTTP header name used to pass the correlation ID.
    /// </summary>
    /// <value>
    ///     The header name is <c>"X-Correlation-Id"</c>.
    /// </value>
    public const string HeaderName = "X-Correlation-Id";

    /// <summary>
    ///     Handles the incoming HTTP request by assigning and propagating a correlation ID.
    /// </summary>
    /// <param name="context">
    ///     The <see cref="HttpContext"/> for the current request.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    /// </returns>
    public async Task Invoke(HttpContext context)
    {
        var correlationId = context.Request.Headers.TryGetValue(HeaderName, out var header) ?
            header.ToString() :
            Guid.NewGuid().ToString("N");

        context.Response.Headers[HeaderName] = correlationId;
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next(context);
        }
    }
}