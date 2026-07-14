using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations.Application.Article;

public sealed class ArticleConfiguration : IEntityTypeConfiguration<Domain.Entities.Application.Article.Article>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Application.Article.Article> builder)
    {
        builder.ToTable("Articles");

        #region Prperties

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.EnglishTitle)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(300);

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.Property(x => x.Content)
            .IsRequired();

        builder.Property(x => x.Summary)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.ImageUrl)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Status)
            .HasConversion<int>();

        builder.Property(x => x.ViewCount)
            .HasDefaultValue(0);

        #endregion

        #region Relations

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Articles)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Articles)
            .HasForeignKey(x => x.ArticleCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Article)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}