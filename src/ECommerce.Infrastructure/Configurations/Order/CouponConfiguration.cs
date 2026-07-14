using ECommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Order;

public sealed class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("Coupons");

        #region Properties

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired();

        builder.Property(x => x.UsedCount)
            .HasDefaultValue(0);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        #endregion

        #region Relations

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Coupon)
            .HasForeignKey(x => x.CouponId)
            .OnDelete(DeleteBehavior.SetNull);

        #endregion
    }
}