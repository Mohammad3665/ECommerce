using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.Common.Extensions;

/// <summary>
/// Provides extension methods for database operations.
/// </summary>
public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IServiceProvider service)
    {
        using var scope = service.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DB Migration");

        try
        {
            logger.LogInformation("Applying database migrations...");

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            context.Database.Migrate();

            logger.LogInformation("Database migrated successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database migration failed.");
            throw;
        }
    }
}