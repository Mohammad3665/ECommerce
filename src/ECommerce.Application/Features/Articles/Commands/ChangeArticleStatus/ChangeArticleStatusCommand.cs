namespace ECommerce.Application.Features.Articles.Commands.ChangeArticleStatus;

public record ChangeArticleStatusCommand(string Slug, string Status) : IRequest<Result>;
