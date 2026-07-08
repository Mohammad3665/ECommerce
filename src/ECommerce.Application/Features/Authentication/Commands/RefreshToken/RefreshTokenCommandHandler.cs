using System.Security.Claims;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Domain.Common.Interfaces;

namespace ECommerce.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    : IRequestHandler<RefreshTokenCommand, Result<TokenResponseDto>>
{
    public async Task<Result<TokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = jwtProvider.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal is null)
            return new Error("Auth.InvalidToken", "ساختار توکن دسترسی نامعتبر است.", ErrorType.Validation);

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ?? principal.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            return new Error("Auth.InvalidClaims", "اطلاعات کاربر در توکن یافت نشد.", ErrorType.Validation);

        var user = await unitOfWork.UserRepository.GetUserWithRolesByIdAsync(userId, cancellationToken);
        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            return new Error("Auth.InvalidRefreshToken", "رفرش توکن منقضی شده یا نامعتبر است.", ErrorType.Forbidden);

        var roles = user.UserRoles.Where(r => r.Role is not null).Select(r => r.Role.Name).ToList();
        var permissions = user.UserRoles
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Name)
            .Distinct();
        var newAccessToken = jwtProvider.GenerateToken(user.Id, user.Email, roles, permissions);
        var newRefreshToken = jwtProvider.GenerateRefreshToken();
        var currentTime = DateTime.UtcNow;
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = currentTime.AddDays(7);

        var expiration = currentTime.AddMinutes(10);

        var result = new TokenResponseDto(newAccessToken, newRefreshToken, expiration);
        return result;
    }
}
