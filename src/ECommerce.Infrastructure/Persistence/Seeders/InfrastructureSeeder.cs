using ECommerce.Domain.Entities.Application.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.Persistence.Seeders;

public static class InfrastructureSeeder
{
    public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


        if (!await context.Roles.AnyAsync())
        {
            var defaultRoles = new List<Role>
            {
                new Role { Name = "SuperAdmin", DisplayName = "مدیر کل", IsDefault = true, IsSystemProtected = true, Level = 100 },
                new Role { Name = "Admin", DisplayName = "مدیر", IsDefault = true, IsSystemProtected = true, Level = 80 },
                new Role { Name = "ContentManager", DisplayName = "مدیر محتوا", IsDefault = true, IsSystemProtected = true, Level = 50 },
                new Role { Name = "Customer", DisplayName = "مشتری", IsDefault = true, IsSystemProtected = true, Level = 10 }
            };

            await context.Roles.AddRangeAsync(defaultRoles);
            await context.SaveChangesAsync();
        }
    }
}
