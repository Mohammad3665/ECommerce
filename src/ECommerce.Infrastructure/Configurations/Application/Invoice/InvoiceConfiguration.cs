using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Application.Invoice;

public sealed class InvoiceConfiguration : IEntityTypeConfiguration<Domain.Entities.Application.Invoice.Invoice>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Application.Invoice.Invoice> builder)
    {
        builder.ToTable("Invoices");

        #region Properties

        builder.Property(x => x.InvoiceNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasIndex(x => x.InvoiceNumber)
            .IsUnique();

        builder.Property(x => x.InvoicePdfUrl)
            .HasMaxLength(300);

        builder.Property(x => x.Status)
            .HasConversion<int>();

        #endregion

        #region Relations

        builder.HasOne(x => x.Order)
            .WithOne()
            .HasForeignKey<Domain.Entities.Application.Invoice.Invoice>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.OrderId)
            .IsUnique();

        #endregion
    }
}