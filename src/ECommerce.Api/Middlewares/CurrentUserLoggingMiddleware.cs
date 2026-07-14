using Serilog.Context;

namespace ECommerce.Api.Middlewares;

/// <summary>
///     Middleware that enriches logs with the current authenticated user's ID.
/// </summary>
/// <remarks>
///     <para>
///         This middleware automatically adds the authenticated user's ID to the
///         logging context for every request. This enables:
///     </para>
/// </remarks>
public class CurrentUserLoggingMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     Processes the HTTP request and enriches logs with the current user's ID.
    /// </summary>
    /// <param name="context">
    ///     The <see cref="HttpContext"/> for the current request.
    /// </param>
    /// <param name="currentUser">
    ///     The <see cref="ICurrentUserService"/> service that provides the current user's information.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    /// </returns>
    public async Task Invoke(HttpContext context, ICurrentUserService currentUser)
    {
        if (context.User.Identity!.IsAuthenticated && currentUser.UserId is not null)
        {
            using (LogContext.PushProperty("UserId", currentUser.UserId))
            {
                await next(context);
            }
            return;
        }

        await next(context);
    }
}