namespace ECommerce.Application.Dtos.Authentication;

public record RefreshTokenRequestDto(string AccessToken, string RefreshToken);
