namespace ECommerce.Application.Dtos.Authentication;

public record TokenResponseDto(string Token, DateTime Expiration);
