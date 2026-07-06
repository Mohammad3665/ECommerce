using ECommerce.Infrastructure.Common.Extensions;

namespace ECommerce.Infrastructure.Persistence.Seeders;

/// <summary>
/// Initializes the database with migrations and seed data.
/// </summary>
public static class DatabaseInitializer
{
    /// <summary>
    /// Applies pending migrations and seeds initial data.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        scope.ServiceProvider.ApplyMigrations();

        await context.SeedDatabaseAsync();
    }
}
