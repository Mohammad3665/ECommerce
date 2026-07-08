using ECommerce.Domain.Entities.Common;
using Mapster;

namespace ECommerce.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<CreateCommentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentCommentId.HasValue)
        {
            var parentExists = await unitOfWork.CommentRepository.IsExistAsync(
                expression: c => c.Id == request.ParentCommentId,
                cancellationToken: cancellationToken
            );
            if (!parentExists)
            {
                var error = new Error(
                    "Comment.ParentNotFound",
                    "دیدگاه اصلی جهت ثبت پاسخ یافت نشد.",
                    ErrorType.NotFound
                );
                return Result<Guid>.Failure(error);
            }
        }

        var isUserAdmin = currentUser.HasPermission("comments.read");

        var comment = request.Adapt<Comment>();
        comment.UserId = (Guid)currentUser.UserId!;

        if (isUserAdmin) comment.IsApproved = true;

        await unitOfWork.CommentRepository.AddAsync(comment, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Comment.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result<Guid>.Failure(error);
        }

        return comment.Id;
    }
}