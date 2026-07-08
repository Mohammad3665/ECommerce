using ECommerce.Application.Dtos.Comments;
using Mapster;

namespace ECommerce.Application.Features.Comments.Queries.GetTargetComments;

public class GetTargetCommentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetTargetCommentsQuery, Result<IEnumerable<CommentDto>>>
{
    public async Task<Result<IEnumerable<CommentDto>>> Handle(GetTargetCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await unitOfWork.CommentRepository.GetAllAsync(
            expression: c => c.IsApproved &&
                ((request.ProductId.HasValue && c.ProductId == request.ProductId) ||
                (request.ArticleId.HasValue && c.ArticleId == request.ArticleId)),
            order: query => query.OrderBy(c => c.CreatedAt),
            cancellationToken: cancellationToken,
            includes: c => c.User
        );

        var allCommentDtos = comments.Adapt<List<CommentDto>>();

        var lookup = allCommentDtos.ToDictionary(c => c.Id);
        var rootComments = new List<CommentDto>();

        foreach (var comment in allCommentDtos)
        {
            if (comment.ParentCommentId.HasValue && lookup.TryGetValue(comment.ParentCommentId.Value, out var parent))
                parent.Replies.Add(comment);

            else
                rootComments.Add(comment);
        }

        return rootComments;
    }
}