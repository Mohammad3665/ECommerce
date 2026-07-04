using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Application.Article;
using Mapster;

namespace ECommerce.Application.Dtos.Articles;

public record CreateArticleRequestDto(
    string Title,
    string EnglishTitle,
    string Content,
    string Summary,
    string Status,
    long ArticleCategoryId
) : IMapTo<Article>, IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Article, CreateArticleRequestDto>()
            .Map(dest => dest.Status, src => src.Status.ToString());
    }
}