using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Order;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.Order.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
    {
        builder.ToTable("Orders");

        #region Properties

        builder.Property(x => x.OrderNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.OrderNumber)
            .IsUnique();

        builder.Property(x => x.OrderDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.SubTotal)
            .IsRequired();

        builder.Property(x => x.DiscountAmount)
            .HasDefaultValue(0);

        builder.Property(x => x.ShippingCost)
            .IsRequired();

        builder.Property(x => x.TotalAmount)
            .IsRequired();

        #endregion

        #region Relations

        builder.HasOne(x => x.User)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}