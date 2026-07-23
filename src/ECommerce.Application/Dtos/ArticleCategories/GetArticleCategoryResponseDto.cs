using ECommerce.Domain.Entities.Application.Article;
using Mapster;

namespace ECommerce.Application.Dtos.ArticleCategories;

public record GetArticleCategoryResponseDto(
    long Id,
    string Name,
    string EnglishName,
    string Slug,
    int ArticleCount
) : IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<ArticleCategory, GetArticleCategoryResponseDto>()
            .Map(dest => dest.ArticleCount, src => src.Articles != null ? src.Articles.Count : 0);
    }
}
