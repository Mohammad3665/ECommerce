using ECommerce.Application.Dtos.Authentication;

namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<TokenResponseDto>>;
