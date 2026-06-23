namespace ECommerce.Application.Dtos.Authentication;

public record ConfirmEmailRequestDto(string Email, string SecurityCode);
