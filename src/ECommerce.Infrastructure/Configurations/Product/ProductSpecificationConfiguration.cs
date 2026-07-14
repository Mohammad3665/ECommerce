using ECommerce.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Product;

public sealed class ProductSpecificationConfiguration : IEntityTypeConfiguration<ProductSpecification>
{
    public void Configure(EntityTypeBuilder<ProductSpecification> builder)
    {
        builder.ToTable("ProductSpecifications");

        #region Properties

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(50);

        #endregion

        #region Indexes

        builder.HasIndex(x => new { x.ProductId, x.Key });

        #endregion
    }

}