namespace ECommerce.Application.Dtos.Authentication;

public record AdminCreateUserRequestDto(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password,
    string Role
);
