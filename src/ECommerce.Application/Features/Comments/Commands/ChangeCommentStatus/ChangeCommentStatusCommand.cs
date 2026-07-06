namespace ECommerce.Application.Features.Comments.Commands.ChangeCommentStatus;

public record ChangeCommentStatusCommand(
    Guid Id,
    bool IsApproved
) : IRequest<Result>;