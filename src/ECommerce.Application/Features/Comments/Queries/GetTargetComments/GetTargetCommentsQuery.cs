using ECommerce.Application.Dtos.Comments;

namespace ECommerce.Application.Features.Comments.Queries.GetTargetComments;

public record GetTargetCommentsQuery(
    long? ProductId,
    long? ArticleId
) : IRequest<Result<IEnumerable<CommentDto>>>;
