namespace ECommerce.Application.Features.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Email, 
    string CurrentPassword, 
    string NewPassword) : IRequest<Result>;
