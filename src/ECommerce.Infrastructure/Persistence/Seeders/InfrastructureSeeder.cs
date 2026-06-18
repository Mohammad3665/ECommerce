using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Seeders;

public static class InfrastructureSeeder
{
    public static async Task SeedDatabaseAsync(this ApplicationDbContext context)
    {
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

        var superAdminEmail = "mashmammad876@gmail.com";
        var hasSuperAdmin = await context.Users.AnyAsync(sa => sa.Email == superAdminEmail);
        if (!hasSuperAdmin)
        {
            var passwordHasher = new BCryptPasswordService();
            var superAdmin = new User
            {
                FullName = "SuperAdmin",
                PhoneNumber = "09024251396",
                Id = Guid.NewGuid(),
                Email = superAdminEmail,
                CreatedAt = DateTime.UtcNow,
                IsEmailConfirmed = true
            };

            superAdmin.PasswordHash = passwordHasher.Hash("SuperSecurePassword123!");

            await context.Users.AddAsync(superAdmin);
            await context.SaveChangesAsync();

            var superAdminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "SuperAdmin");
            if (superAdminRole is not null)
            {
                await context.UserRoles.AddAsync(new UserRole
                {
                    UserId = superAdmin.Id,
                    RoleId = superAdminRole.Id
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
