using ECommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Order;

public sealed class OrderShippingConfiguration : IEntityTypeConfiguration<OrderShipping>
{
    public void Configure(EntityTypeBuilder<OrderShipping> builder)
    {
        builder.ToTable("OrderShippings");

        #region Properties

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.PostalCode)
            .IsRequired()
            .HasMaxLength(20);

        #endregion

        #region Relations

        builder.HasOne(x => x.Order)
            .WithOne()
            .HasForeignKey<OrderShipping>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}