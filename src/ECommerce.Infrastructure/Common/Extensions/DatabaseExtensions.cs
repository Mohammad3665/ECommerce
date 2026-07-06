namespace ECommerce.Infrastructure.Common.Extensions;

/// <summary>
/// Provides database migration extension methods.
/// </summary>
public static class DatabaseExtensions
{
    /// <summary>
    /// Applies pending migrations to the database.
    /// </summary>
    /// <param name="service">The service provider.</param>
    /// <remarks>
    /// Creates a scoped service, retrieves the database context, and applies any pending migrations.
    /// Logs success or failure with detailed error information.
    /// </remarks>
    public static void ApplyMigrations(this IServiceProvider service)
    {
        using var scope = service.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DB Migration");

        try
        {
            logger.LogInformation("🚀 Applying database migrations...");

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            logger.LogInformation("✅ Database migrated successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "💥 Database migration failed.");
            throw;
        }
    }
}