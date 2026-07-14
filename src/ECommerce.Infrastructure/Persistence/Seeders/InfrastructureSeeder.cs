using ECommerce.Application.Authorization;
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
            new Permission { Name = Permissions.Users.Create, Module = "User Management", Description = "امکان ساخت کاربر جدید" },
            new Permission { Name = Permissions.Users.Read, Module = "User Management", Description = "امکان مشاهده لیست و جزئیات کاربران" },
            new Permission { Name = Permissions.Users.Update, Module = "User Management", Description = "امکان ویرایش اطلاعات کاربران" },
            new Permission { Name = Permissions.Users.Delete, Module = "User Management", Description = "امکان حذف نرم یا غیرفعال‌سازی کاربران" },

            // Role Management
            new Permission { Name = Permissions.Roles.Create, Module = "Role Management", Description = "امکان ساخت نقش جدید" },
            new Permission { Name = Permissions.Roles.Read, Module = "Role Management", Description = "امکان مشاهده نقش‌ها و دسترسی‌ها" },
            new Permission { Name = Permissions.Roles.Update, Module = "Role Management", Description = "امکان ویرایش نقش‌ها و تغییر دسترسی‌ها" },
            new Permission { Name = Permissions.Roles.Delete, Module = "Role Management", Description = "امکان حذف نقش‌های سفارشی سیستم" },

            // Product Management
            new Permission { Name = Permissions.Products.Create, Module = "Product Management", Description = "امکان ثبت محصول جدید" },
            new Permission { Name = Permissions.Products.Read, Module = "Product Management", Description = "امکان مشاهده محصولات پنل مدیریت" },
            new Permission { Name = Permissions.Products.Update, Module = "Product Management", Description = "امکان ویرایش اطلاعات محصولات" },
            new Permission { Name = Permissions.Products.Delete, Module = "Product Management", Description = "امکان حذف محصولات" },

            // Category Management
            new Permission { Name = Permissions.Categories.Create, Module = "Category Management", Description = "امکان ساخت دسته‌بندی جدید" },
            new Permission { Name = Permissions.Categories.Read, Module = "Category Management", Description = "امکان مشاهده دسته‌بندی‌ها" },
            new Permission { Name = Permissions.Categories.Update, Module = "Category Management", Description = "امکان ویرایش دسته‌بندی‌ها" },
            new Permission { Name = Permissions.Categories.Delete, Module = "Category Management", Description = "امکان حذف دسته‌بندی‌ها" },

            // Brand Management
            new Permission { Name = Permissions.Brands.Create, Module = "Brand Management", Description = "امکان ثبت برند جدید" },
            new Permission { Name = Permissions.Brands.Read, Module = "Brand Management", Description = "امکان مشاهده برندها" },
            new Permission { Name = Permissions.Brands.Update, Module = "Brand Management", Description = "امکان ویرایش اطلاعات برندها" },
            new Permission { Name = Permissions.Brands.Delete, Module = "Brand Management", Description = "امکان حذف برندها" },

            // Order Management
            new Permission { Name = Permissions.Orders.Read, Module = "Order Management", Description = "امکان مشاهده و فیلتر سفارشات مشتریان" },
            new Permission { Name = Permissions.Orders.Update, Module = "Order Management", Description = "امکان ویرایش وضعیت سفارشات" },
            new Permission { Name = Permissions.Orders.Cancel, Module = "Order Management", Description = "امکان لغو سفارشات مشتریان" },

            // Comment Management
            new Permission { Name = Permissions.Comments.Read, Module = "Comment Management", Description = "امکان مشاهده نظرات کاربران روی محصولات/مقالات" },
            new Permission { Name = Permissions.Comments.Approve, Module = "Comment Management", Description = "امکان تایید نظرات جهت نمایش در سایت" },
            new Permission { Name = Permissions.Comments.Reject, Module = "Comment Management", Description = "امکان رد کردن نظرات کاربران" },
            new Permission { Name = Permissions.Comments.Delete, Module = "Comment Management", Description = "امکان حذف کامل نظرات" },

            // Article Management
            new Permission { Name = Permissions.Articles.Create, Module = "Article Management", Description = "امکان ایجاد و انتشار مقاله در وبلاگ" },
            new Permission { Name = Permissions.Articles.Read, Module = "Article Management", Description = "امکان مشاهده لیست مقالات پنل" },
            new Permission { Name = Permissions.Articles.Update, Module = "Article Management", Description = "امکان ویرایش محتوای مقالات" },
            new Permission { Name = Permissions.Articles.Delete, Module = "Article Management", Description = "امکان حذف مقالات وبلاگ" },

            // Slider Management
            new Permission { Name = Permissions.Sliders.Create, Module = "Slider Management", Description = "امکان افزودن اسلاید جدید به صفحه اصلی" },
            new Permission { Name = Permissions.Sliders.Read, Module = "Slider Management", Description = "امکان مشاهده اسلایدرهای وب‌سایت" },
            new Permission { Name = Permissions.Sliders.Update, Module = "Slider Management", Description = "امکان ویرایش تصاویر و لینک‌های اسلایدر" },
            new Permission { Name = Permissions.Sliders.Delete, Module = "Slider Management", Description = "امکان حذف اسلایدرها" },

            // Coupon Management
            new Permission { Name = Permissions.Coupons.Create, Module = "Coupon Management", Description = "امکان تعریف کد تخفیف جدید" },
            new Permission { Name = Permissions.Coupons.Read, Module = "Coupon Management", Description = "امکان مشاهده کدهای تخفیف تعریف شده" },
            new Permission { Name = Permissions.Coupons.Update, Module = "Coupon Management", Description = "امکان ویرایش شرایط و تاریخ کدهای تخفیف" },
            new Permission { Name = Permissions.Coupons.Delete, Module = "Coupon Management", Description = "امکان حذف کدهای تخفیف" },

            // Dashboard
            new Permission { Name = Permissions.Dashboard.View, Module = "Dashboard", Description = "امکان ورود و مشاهده آمارهای کلی داشبورد مدیریت" }
        };
    }

}
