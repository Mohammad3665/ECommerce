using ECommerce.Application.Dtos.Authentication;
using ECommerce.Domain.Common.Interfaces;

namespace ECommerce.Application.Features.Authentication.Queries.Login;

public class LoginQueryHandler(
    IUnitOfWork unitOfWork,
    IPasswordService passwordService,
    IJwtProvider jwtProvider) : IRequestHandler<LoginQuery, Result<TokenResponseDto>>
{
    public async Task<Result<TokenResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByEmailAsync(
            request.Email,
            cancellationToken
        );
        if (user is null)
            return new Error("Auth.InvalidCredentials", "Email or password is wrong.", ErrorType.Validation);

        if (!user.IsActive || !user.IsEmailConfirmed)
            return new Error("Auth.UserInactive", "Your account is not active.", ErrorType.Forbidden);

        var isPasswordValid = passwordService.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
            return new Error("Auth.InvalidCredentials", "ایمیل یا رمز عبور اشتباه است.", ErrorType.Validation);

        var roles = user.UserRoles.Where(ur => ur.Role is not null).Select(r => r.Role.Name).ToList();
        var permissions = user.UserRoles
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Name)
            .Distinct();
        var token = jwtProvider.GenerateToken(user.Id, user.Email, roles, permissions);
        var currentTime = DateTime.UtcNow;
        var expiration = currentTime.AddMinutes(10);

        user.LastLoginAt = currentTime;

        var newRefreshToken = jwtProvider.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = currentTime.AddDays(7);

        unitOfWork.UserRepository.Update(user);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        if (saveResult.IsFailure)
            return new Error("Auth.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);

        var result = new TokenResponseDto(token, newRefreshToken, expiration);
        return result;
    }
}
