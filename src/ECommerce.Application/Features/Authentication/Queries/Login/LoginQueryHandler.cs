using System.Linq.Expressions;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Interfaces;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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
        {
            var error = new Error(
                "Auth.InvalidCredentials",
                "Email or password is wrong.",
                ErrorType.Validation
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        if (!user.IsActive || !user.IsEmailConfirmed)
        {
            var error = new Error(
                "Auth.UserInactive",
                "Your account is not active.",
                ErrorType.Forbidden
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        var isPasswordValid = passwordService.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            var error = new Error(
                "Auth.InvalidCredentials",
                "ایمیل یا رمز عبور اشتباه است.",
                ErrorType.Validation
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        var roles = user.UserRoles.Where(ur => ur.Role is not null).Select(r => r.Role.Name).ToList();
        var permissions = user.UserRoles
            .SelectMany(ur => ur.Role.RolePermissions)
            .Select(rp => rp.Permission.Name)
            .Distinct();
        Console.WriteLine($"DEBUG: Roles Count = {roles.Count}, Permissions Count = {permissions.Count()}");
        var token = jwtProvider.GenerateToken(user.Id, user.Email, roles, permissions);
        var currentTime = DateTime.UtcNow;
        var expiration = currentTime.AddMinutes(10);

        user.LastLoginAt = currentTime;

        var newRefreshToken = jwtProvider.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = currentTime.AddDays(7);

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveAsync(cancellationToken);

        var result = new TokenResponseDto(token, newRefreshToken, expiration);
        return Result<TokenResponseDto>.Success(result);
    }
}
