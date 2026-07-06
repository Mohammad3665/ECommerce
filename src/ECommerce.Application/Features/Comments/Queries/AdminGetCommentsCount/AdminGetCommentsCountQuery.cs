namespace ECommerce.Application.Features.Comments.Queries.AdminGetCommentsCount;

public record AdminGetCommentsCountQuery(
    long? ProductId,
    long? ArticleId
) : IRequest<Result<int>>;