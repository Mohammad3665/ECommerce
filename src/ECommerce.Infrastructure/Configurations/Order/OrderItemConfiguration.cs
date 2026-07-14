using ECommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Order;

public sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        #region Properties

        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.UnitPrice)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.TotalPrice)
            .IsRequired();

        #endregion

        #region Relations

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Indexes

        builder.HasIndex(x => x.OrderId);

        builder.HasIndex(x => x.ProductId);

        #endregion
    }
}