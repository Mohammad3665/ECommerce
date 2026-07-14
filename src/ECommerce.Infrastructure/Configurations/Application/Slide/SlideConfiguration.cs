using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Application.Slide;

public sealed class SlideConfiguration : IEntityTypeConfiguration<Domain.Entities.Application.Slide.Slide>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Application.Slide.Slide> builder)
    {
        builder.ToTable("Slides");

        #region Properties

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.EnglishTitle)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Link)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.DisplayOrder)
            .HasDefaultValue(0);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.HasIndex(x => x.DisplayOrder);

        #endregion
    }
}