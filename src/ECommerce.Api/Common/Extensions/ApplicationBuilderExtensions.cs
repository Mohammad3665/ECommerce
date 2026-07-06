using Scalar.AspNetCore;
using Serilog;

namespace ECommerce.Api.Common.Extensions;

/// <summary>
/// Configures the application's middleware pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Registers all required middlewares for the application.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    /// <returns>The configured application builder.</returns>
    /// <remarks>
    /// Configures HTTPS, logging, API documentation, exception handling, static files, and controllers.
    /// HTTPS redirection and HSTS are disabled in Docker environments.
    /// </remarks>
    public static async Task<IApplicationBuilder> UseApplicationMiddlewares(this WebApplication app)
    {
        var isInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        app.UseSerilogRequestLogging();

        if (!isInDocker)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            app.MapGet("/test", () => Results.Json(new { message = "api is working" }));
            app.MapGet("/", () => Results.Redirect("/scalar"));
        }
        app.UseExceptionHandler();
        app.UseStaticFiles();
        app.MapControllers();

        return app;
    }
}
