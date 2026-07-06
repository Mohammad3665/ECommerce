namespace ECommerce.Application.Features.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string Email, string SecurityCode) : IRequest<Result>;
