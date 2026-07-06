namespace ECommerce.Application.Features.Users.Commands.ToggleUserStatus;

public record ToggleUserStatusCommand(Guid UserId, bool IsActive) : IRequest<Result>;
