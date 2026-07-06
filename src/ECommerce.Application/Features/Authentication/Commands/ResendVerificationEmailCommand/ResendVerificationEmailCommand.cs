namespace ECommerce.Application.Features.Authentication.Commands.ResendVerificationEmailCommand;

public record ResendVerificationEmailCommand(string Email) : IRequest<Result>;
