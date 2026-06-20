namespace ECommerce.Application.Dtos.Authentication;

public record ResetForgotedPasswordRequestDto(
    string Email,
    string SecurityCode,
    string NewPassword);
