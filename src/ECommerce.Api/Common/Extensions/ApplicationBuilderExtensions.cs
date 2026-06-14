using ECommerce.Infrastructure.Common.Extensions;
using Scalar.AspNetCore;
using Serilog;

namespace ECommerce.Api.Common.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplicationMiddlewares(this WebApplication app)
    {
        var isInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        app.UseSerilogRequestLogging();
        app.MapControllers();

        app.Services.ApplyMigrations();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            app.MapGet("/test", () => Results.Json(new { message = "api is working"}));
            app.MapGet("/", () => Results.Redirect("/scalar"));
        }

        if (!isInDocker)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        return app;
    }
}
