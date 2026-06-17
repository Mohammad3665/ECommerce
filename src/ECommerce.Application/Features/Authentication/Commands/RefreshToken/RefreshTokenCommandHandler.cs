using System.Security.Claims;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Interfaces;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal is null)
        {
            var error = new Error(
                "Auth.InvalidToken", 
                "Invalid access token structure.", 
                ErrorType.Validation
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            var error = new Error(
                "Auth.InvalidClaims", 
                "User metadata missing from token.", 
                ErrorType.Validation
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        var user = await unitOfWork.UserRepository.GetUserWithRolesByIdAsync(userId, cancellationToken);
        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            var error = new Error(
                "Auth.InvalidRefreshToken", 
                "Refresh token is invalid or expired.", 
                ErrorType.Forbidden
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        var roles = user.UserRoles.Where(r => r.Role is not null).Select(r => r.Role.Name).ToList();
        var newAccessToken = jwtProvider.GenerateToken(user.Id, user.Email, roles);
        var newRefreshToken = jwtProvider.GenerateRefreshToken();
        var currentTime = DateTime.UtcNow;
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = currentTime.AddDays(7);

        var expiration = currentTime.AddMinutes(10);

        var result = new TokenResponseDto(newAccessToken, newRefreshToken, expiration);
        return Result<TokenResponseDto>.Success(result);
    }
}
