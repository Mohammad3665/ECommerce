using ECommerce.Infrastructure.Common.Extensions;
using ECommerce.Infrastructure.Persistence.Seeders;
using Scalar.AspNetCore;
using Serilog;

namespace ECommerce.Api.Common.Extensions;

public static class ApplicationBuilderExtensions
{
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
            app.MapGet("/test", () => Results.Json(new { message = "api is working"}));
            app.MapGet("/", () => Results.Redirect("/scalar"));
        }

        app.MapControllers();

        await app.Services.InitializeDatabaseAsync();

        return app;
    }
}
