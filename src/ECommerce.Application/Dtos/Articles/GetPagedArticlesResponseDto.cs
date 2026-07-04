using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Application.Article;

namespace ECommerce.Application.Dtos.Articles;

public record GetPagedArticlesResponseDto(
    long Id,
    string Slug,
    string Title,
    string Summary,
    string ImageUrl,
    Guid AuthorId,
    string AuthorName
) : IMapFrom<Article>, IHaveCustomMapping
{
    public static void ConfigureMapping(Mapster.TypeAdapterConfig config)
    {
        config.NewConfig<Article, GetPagedArticlesResponseDto>()
            .Map(dest => dest.AuthorName, src => src.Author.FullName);
    }
}