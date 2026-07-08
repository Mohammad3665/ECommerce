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
            return new Error("Comment.NotFound", "دیدگاه مورد نظر یافت نشد.", ErrorType.NotFound);

        if (comment.IsApproved)
            return new Error("Comment.AlreadyApproved", "این دیدگاه قبلاً تایید شده است و قابل رد کردن (ریجکت) نیست. در صورت نیاز از گزینه حذف استفاده کنید.", ErrorType.Validation);

        unitOfWork.CommentRepository.DeletePermanently(comment);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Comment.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
