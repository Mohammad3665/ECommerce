namespace ECommerce.Application.Features.Authentication.Commands.Logout;

public record LogoutCommand(Guid UserId) : IRequest<Result>;
