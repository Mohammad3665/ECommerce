using ECommerce.Domain.Entities.Application.Article;
using ECommerce.Domain.Entities.Application.Invoice;
using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.Entities.Application.Slide;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    #region Db Sets

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Permission> Permissions { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductImage> ProductImages { get; set; } = null!;
    public DbSet<ProductSpecification> ProductSpecifications { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<OrderHistory> OrderHistories { get; set; } = null!;
    public DbSet<OrderPayment> OrderPayments { get; set; } = null!;
    public DbSet<OrderShipping> OrderShippingInfos { get; set; } = null!;
    public DbSet<Coupon> Coupons { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Article> Articles { get; set; } = null!;
    public DbSet<ArticleCategory> ArticleCategories { get; set; } = null!;
    public DbSet<Slide> Slides { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;

    #endregion

    #region Model Creating

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId});

            entity.HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ur => ur.AssignedBy)
                .WithMany()
                .HasForeignKey(ur => ur.AssignedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasOne(i => i.Order)
                .WithOne()
                .HasForeignKey<Invoice>(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(i => i.InvoiceNumber).IsUnique();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(o => o.OrderNumber).IsUnique();
        
            entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasOne(oi => oi.Product)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Article>()
            .HasOne(a => a.Author)
            .WithMany(u => u.Articles)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Order)
            .WithOne()
            .HasForeignKey<Invoice>(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Invoice>().HasIndex(i => i.InvoiceNumber).IsUnique();
        modelBuilder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();
    }

    #endregion

    #region Configure Conventions

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    #endregion
}