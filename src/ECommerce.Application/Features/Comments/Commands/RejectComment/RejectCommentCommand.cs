namespace ECommerce.Application.Features.Comments.Commands.RejectComment;

public record RejectCommentCommand(Guid Id) : IRequest<Result>;
