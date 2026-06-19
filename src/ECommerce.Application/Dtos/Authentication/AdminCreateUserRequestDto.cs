namespace ECommerce.Application.Dtos.Authentication;

public record CreateUserByAdminRequestDto(
    string FullName,
    string Email,
    string PhoneNumber,
    string? Password,
    string Role
);
