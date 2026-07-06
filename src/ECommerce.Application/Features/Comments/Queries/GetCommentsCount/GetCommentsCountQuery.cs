namespace ECommerce.Application.Features.Comments.Queries.GetCommentsCount;

public record GetCommentsCountQuery(
    long? ProductId,
    long? ArticleId
) : IRequest<Result<int>>;
