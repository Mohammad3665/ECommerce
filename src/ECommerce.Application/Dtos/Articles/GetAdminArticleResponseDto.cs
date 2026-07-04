using System.Text.Json.Serialization;
using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Application.Article;
using ECommerce.Domain.Enums;
using Mapster;

namespace ECommerce.Application.Dtos.Articles;

public record GetAdminArticleResponseDto(
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
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    ArticleStatus Status,
    DateTime? PublishedAt,
    DateTime? ArchivedAt
) : IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Article, GetAdminArticleResponseDto>()
            .Map(dest => dest.AuthorName, src => src.Author.FullName);
    }
}