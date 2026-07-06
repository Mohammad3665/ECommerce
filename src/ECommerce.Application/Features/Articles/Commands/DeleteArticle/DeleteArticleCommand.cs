namespace ECommerce.Application.Features.Articles.Commands.DeleteArticle;

public record DeleteArticleCommand(string Slug) : IRequest<Result>;
