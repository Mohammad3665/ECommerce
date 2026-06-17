namespace ECommerce.Application.Dtos.Authentication;

public record TokenResponseDto(string AccessToken, string RefreshToken, DateTime Expiration);
