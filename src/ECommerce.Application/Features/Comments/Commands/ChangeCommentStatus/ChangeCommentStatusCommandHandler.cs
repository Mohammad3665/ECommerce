namespace ECommerce.Application.Features.Comments.Commands.ChangeCommentStatus;

public class ChangeCommentStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeCommentStatusCommand, Result>
{
    public async Task<Result> Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
    {
        var comment = await unitOfWork.CommentRepository.GetAsync(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (comment is null)
            return new Error("Comment.NotFound", "دیدگاه مورد نظر یافت نشد.", ErrorType.NotFound);

        comment.IsApproved = request.IsApproved;
        comment.ApprovedAt = request.IsApproved ? DateTime.UtcNow : null;

        unitOfWork.CommentRepository.Update(comment);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Comment.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
