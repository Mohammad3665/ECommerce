using ECommerce.Application.Dtos.Authentication;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<Result<TokenResponseDto>>;
