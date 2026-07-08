using ECommerce.Domain.Entities.Common;

namespace ECommerce.Application.Features.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<DeleteCommentCommand, Result>
{
    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var rootComment = await unitOfWork.CommentRepository.GetAsync(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (rootComment is null)
            return new Error("Comment.NotFound", "دیدگاه یافت نشد.", ErrorType.NotFound);

        Guid currentUserId = (Guid)currentUser.UserId!;
        if (rootComment.UserId != currentUserId)
            return new Error("Comment.Forbidden", "شما مجاز به حذف این دیدگاه نیستید.", ErrorType.Forbidden);

        var allComments = await unitOfWork.CommentRepository.GetAllAsync(
            expression: c => c.ProductId == rootComment.ProductId && c.ArticleId == rootComment.ArticleId,
            cancellationToken: cancellationToken
        );

        var commentsToDelete = new List<Comment> { rootComment };
        rootComment.CollectAllRepliesRecursive(allComments.ToList(), commentsToDelete);

        foreach (var comment in commentsToDelete)
            unitOfWork.CommentRepository.DeletePermanently(comment);

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Comment.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
