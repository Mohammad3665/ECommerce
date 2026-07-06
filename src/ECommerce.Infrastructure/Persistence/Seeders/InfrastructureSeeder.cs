using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Infrastructure.Common.Services;

namespace ECommerce.Infrastructure.Persistence.Seeders;

/// <summary>
/// Seeds initial data for the database including roles, permissions, and super admin user.
/// </summary>
public static class InfrastructureSeeder
{
    /// <summary>
    /// Seeds the database with default roles, permissions, and a super admin user.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedDatabaseAsync(this ApplicationDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            var defaultRoles = new List<Role>
            {
                new Role { Name = "SuperAdmin", DisplayName = "مدیر کل", Slug = "super-admin", IsDefault = true, IsSystemProtected = true, Level = 100 },
                new Role { Name = "Admin", DisplayName = "مدیر", Slug = "admin", IsDefault = true, IsSystemProtected = true, Level = 80 },
                new Role { Name = "ContentManager", DisplayName = "مدیر محتوا", Slug = "content-manager", IsDefault = true, IsSystemProtected = true, Level = 50 },
                new Role { Name = "Customer", DisplayName = "مشتری", Slug = "customer", IsDefault = true, IsSystemProtected = true, Level = 10 }
            };

            await context.Roles.AddRangeAsync(defaultRoles);
            await context.SaveChangesAsync();
        }

        if (!await context.Permissions.AnyAsync())
        {
            var allPermissions = GetPermissionsList();
            await context.Permissions.AddRangeAsync(allPermissions);
            await context.SaveChangesAsync();
        }

        var superAdminEmail = "mashmammad876@gmail.com";
        var superAdmin = await context.Users.FirstOrDefaultAsync(sa => sa.Email == superAdminEmail);

        if (superAdmin is null)
        {
            var passwordHasher = new BCryptPasswordService();
            superAdmin = new User
            {
                FullName = "SuperAdmin",
                PhoneNumber = "09024251396",
                Id = Guid.NewGuid(),
                Email = superAdminEmail,
                CreatedAt = DateTime.UtcNow,
                IsEmailConfirmed = true,
                IsActive = true
            };

            superAdmin.PasswordHash = passwordHasher.Hash("SuperSecurePassword123!");

            await context.Users.AddAsync(superAdmin);
            await context.SaveChangesAsync();
        }

        var superAdminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "SuperAdmin");
        if (superAdminRole is not null)
        {
            var hasRoleAssigned = await context.UserRoles.AnyAsync(ur => ur.UserId == superAdmin.Id && ur.RoleId == superAdminRole.Id);
            if (!hasRoleAssigned)
            {
                await context.UserRoles.AddAsync(new UserRole
                {
                    UserId = superAdmin.Id,
                    RoleId = superAdminRole.Id,
                    AssignedAt = DateTime.UtcNow
                });
                await context.SaveChangesAsync();
            }

            var hasAnyPermissionsMapped = await context.RolePermissions.AnyAsync(rp => rp.RoleId == superAdminRole.Id);
            if (!hasAnyPermissionsMapped)
            {
                var allPermissionIds = await context.Permissions.Select(p => p.Id).ToListAsync();

                var rolePermissions = allPermissionIds.Select(pId => new RolePermission
                {
                    RoleId = superAdminRole.Id,
                    PermissionId = pId
                }).ToList();

                await context.RolePermissions.AddRangeAsync(rolePermissions);
                await context.SaveChangesAsync();
            }
        }
    }

    private static List<Permission> GetPermissionsList()
    {
        return new List<Permission>
        {
            // User Management
            new Permission { Name = "users.create", Module = "User Management", Description = "امکان ساخت کاربر جدید" },
            new Permission { Name = "users.read", Module = "User Management", Description = "امکان مشاهده لیست و جزئیات کاربران" },
            new Permission { Name = "users.update", Module = "User Management", Description = "امکان ویرایش اطلاعات کاربران" },
            new Permission { Name = "users.delete", Module = "User Management", Description = "امکان حذف نرم یا غیرفعال‌سازی کاربران" },

            // Role Management
            new Permission { Name = "roles.create", Module = "Role Management", Description = "امکان ساخت نقش جدید" },
            new Permission { Name = "roles.read", Module = "Role Management", Description = "امکان مشاهده نقش‌ها و دسترسی‌ها" },
            new Permission { Name = "roles.update", Module = "Role Management", Description = "امکان ویرایش نقش‌ها و تغییر دسترسی‌ها" },
            new Permission { Name = "roles.delete", Module = "Role Management", Description = "امکان حذف نقش‌های سفارشی سیستم" },

            // Product Management
            new Permission { Name = "products.create", Module = "Product Management", Description = "امکان ثبت محصول جدید" },
            new Permission { Name = "products.read", Module = "Product Management", Description = "امکان مشاهده محصولات پنل مدیریت" },
            new Permission { Name = "products.update", Module = "Product Management", Description = "امکان ویرایش اطلاعات محصولات" },
            new Permission { Name = "products.delete", Module = "Product Management", Description = "امکان حذف محصولات" },

            // Category Management
            new Permission { Name = "categories.create", Module = "Category Management", Description = "امکان ساخت دسته‌بندی جدید" },
            new Permission { Name = "categories.read", Module = "Category Management", Description = "امکان مشاهده دسته‌بندی‌ها" },
            new Permission { Name = "categories.update", Module = "Category Management", Description = "امکان ویرایش دسته‌بندی‌ها" },
            new Permission { Name = "categories.delete", Module = "Category Management", Description = "امکان حذف دسته‌بندی‌ها" },

            // Brand Management
            new Permission { Name = "brands.create", Module = "Brand Management", Description = "امکان ثبت برند جدید" },
            new Permission { Name = "brands.read", Module = "Brand Management", Description = "امکان مشاهده برندها" },
            new Permission { Name = "brands.update", Module = "Brand Management", Description = "امکان ویرایش اطلاعات برندها" },
            new Permission { Name = "brands.delete", Module = "Brand Management", Description = "امکان حذف برندها" },

            // Order Management
            new Permission { Name = "orders.read", Module = "Order Management", Description = "امکان مشاهده و فیلتر سفارشات مشتریان" },
            new Permission { Name = "orders.update", Module = "Order Management", Description = "امکان ویرایش وضعیت سفارشات" },
            new Permission { Name = "orders.cancel", Module = "Order Management", Description = "امکان لغو سفارشات مشتریان" },

            // Comment Management
            new Permission { Name = "comments.read", Module = "Comment Management", Description = "امکان مشاهده نظرات کاربران روی محصولات/مقالات" },
            new Permission { Name = "comments.approve", Module = "Comment Management", Description = "امکان تایید نظرات جهت نمایش در سایت" },
            new Permission { Name = "comments.reject", Module = "Comment Management", Description = "امکان رد کردن نظرات کاربران" },
            new Permission { Name = "comments.delete", Module = "Comment Management", Description = "امکان حذف کامل نظرات" },

            // Article Management
            new Permission { Name = "articles.create", Module = "Article Management", Description = "امکان ایجاد و انتشار مقاله در وبلاگ" },
            new Permission { Name = "articles.read", Module = "Article Management", Description = "امکان مشاهده لیست مقالات پنل" },
            new Permission { Name = "articles.update", Module = "Article Management", Description = "امکان ویرایش محتوای مقالات" },
            new Permission { Name = "articles.delete", Module = "Article Management", Description = "امکان حذف مقالات وبلاگ" },

            // Slider Management
            new Permission { Name = "sliders.create", Module = "Slider Management", Description = "امکان افزودن اسلاید جدید به صفحه اصلی" },
            new Permission { Name = "sliders.read", Module = "Slider Management", Description = "امکان مشاهده اسلایدرهای وب‌سایت" },
            new Permission { Name = "sliders.update", Module = "Slider Management", Description = "امکان ویرایش تصاویر و لینک‌های اسلایدر" },
            new Permission { Name = "sliders.delete", Module = "Slider Management", Description = "امکان حذف اسلایدرها" },

            // Coupon Management
            new Permission { Name = "coupons.create", Module = "Coupon Management", Description = "امکان تعریف کد تخفیف جدید" },
            new Permission { Name = "coupons.read", Module = "Coupon Management", Description = "امکان مشاهده کدهای تخفیف تعریف شده" },
            new Permission { Name = "coupons.update", Module = "Coupon Management", Description = "امکان ویرایش شرایط و تاریخ کدهای تخفیف" },
            new Permission { Name = "coupons.delete", Module = "Coupon Management", Description = "امکان حذف کدهای تخفیف" },

            // Dashboard
            new Permission { Name = "dashboard.view", Module = "Dashboard", Description = "امکان ورود و مشاهده آمارهای کلی داشبورد مدیریت" }
        };
    }

}
