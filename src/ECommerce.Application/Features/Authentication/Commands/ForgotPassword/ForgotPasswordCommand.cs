namespace ECommerce.Application.Features.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<Result>;
