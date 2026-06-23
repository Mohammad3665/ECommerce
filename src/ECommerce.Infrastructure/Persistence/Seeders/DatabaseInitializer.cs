using ECommerce.Infrastructure.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Persistence.Seeders;

public static class DatabaseInitializer
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        scope.ServiceProvider.ApplyMigrations();

        await context.SeedDatabaseAsync();
    }
}
