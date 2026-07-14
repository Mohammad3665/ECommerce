using ECommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Order;

public sealed class OrderPaymentConfiguration : IEntityTypeConfiguration<OrderPayment>
{
    public void Configure(EntityTypeBuilder<OrderPayment> builder)
    {
        builder.ToTable("OrderPayments");

        #region Properties

        builder.Property(x => x.PaymentMethod)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.TransactionId)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.TransactionId)
            .IsUnique();

        builder.Property(x => x.IsPaid)
            .HasDefaultValue(false);

        #endregion

        #region Relations

        builder.HasOne(x => x.Order)
            .WithOne()
            .HasForeignKey<OrderPayment>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Order)
            .WithOne(x => x.Payment)
            .HasForeignKey<OrderPayment>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}