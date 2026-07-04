namespace ECommerce.Application.Dtos.Articles;

public record EditArticleRequestDto(
    string Title,
    string EnglishTitle,
    string Content,
    string Summary,
    long ArticleCategoryId
);