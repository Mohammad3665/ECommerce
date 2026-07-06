namespace ECommerce.Application.Features.Comments.Commands.DeleteCommentByAdmin;

public record DeleteCommentByAdminCommand(Guid Id) : IRequest<Result>;
