using ECommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Order;

public sealed class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.ToTable("OrderHistories");

        #region Properties

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Note)
            .HasMaxLength(300);

        builder.Property(x => x.ChangedAt)
            .IsRequired();

        #endregion

        #region Relations

        builder.HasOne(x => x.Order)
            .WithMany(x => x.Histories)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ChangedByUser)
            .WithMany()
            .HasForeignKey(x => x.ChangedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        #endregion
    }
}