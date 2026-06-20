namespace ECommerce.Application.Dtos.Authentication;

public record ResetPasswordRequestDto(
    string Email, 
    string CurrentPassword, 
    string NewPassword);
