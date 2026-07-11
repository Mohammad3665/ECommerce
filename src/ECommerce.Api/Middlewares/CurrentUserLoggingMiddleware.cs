using Serilog.Context;

namespace ECommerce.Api.Middlewares;

public class CurrentUserLoggingMiddleware(RequestDelegate next)
{
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
