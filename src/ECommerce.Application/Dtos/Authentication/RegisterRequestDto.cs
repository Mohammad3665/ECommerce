namespace ECommerce.Application.Dtos.Authentication;

public record RegisterRequestDto(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password
);