namespace ECommerce.Application.Features.Comments.Commands.RejectComment;

public class RejectCommentCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RejectCommentCommand, Result>
{
    public async Task<Result> Handle(RejectCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await unitOfWork.CommentRepository.GetAsync(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (comment is null)
        {
            var error = new Error(
                "Comment.NotFound",
                "دیدگاه مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        if (comment.IsApproved)
        {
            var error = new Error(
                "Comment.AlreadyApproved",
                "این دیدگاه قبلاً تایید شده است و قابل رد کردن (ریجکت) نیست. در صورت نیاز از گزینه حذف استفاده کنید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        unitOfWork.CommentRepository.DeletePermanently(comment);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Comment.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        return Result.Success();
    }
}