using ECommerce.Domain.Entities.Application.Article;
using Mapster;

namespace ECommerce.Application.Dtos.Articles;

public record GetArticleResponseDto(
    long Id,
    string Slug,
    string Title,
    string EnglishTitle,
    string Content,
    string Summary,
    string ImageUrl,
    int ViewCount,
    Guid AuthorId,
    string AuthorName,
    DateTime? PublishedAt
) : IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Article, GetArticleResponseDto>()
            .Map(dest => dest.AuthorName, src => src.Author.FullName);
    }
}