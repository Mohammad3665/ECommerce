using ECommerce.Domain.Entities.Application.Article;

namespace ECommerce.Application.Dtos.ArticleCategories;

public record GetArticleCategoryResponseDto(long Id, string Name, string EnglishName, string Slug) : IMapFrom<ArticleCategory>;
