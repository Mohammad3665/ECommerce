using ECommerce.Domain.Entities.Common;

namespace ECommerce.Application.Features.Comments.Commands.CreateComment;

public record CreateCommentCommand(
    string Title,
    string Content,
    Guid UserId,
    long? ProductId,
    long? ArticleId,
    Guid? ParentCommentId
) : IMapTo<Comment>, IRequest<Result<Guid>>;
