using System.Linq.Expressions;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Interfaces;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Queries;

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

        if (!user.IsActive)
        {
            var error = new Error(
                "Auth.UserInactive",
                "Your account is deactived.",
                ErrorType.Forbidden
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        var isPasswordValid = passwordService.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            var error = new Error(
                "Auth.InvalidCredentials",
                "Email or password is wrong.",
                ErrorType.Validation
            );
            return Result<TokenResponseDto>.Failure(error);
        }

        var roles = user.UserRoles.Where(ur => ur.Role is not null).Select(r => r.Role.Name).ToList();
        var token = jwtProvider.GenerateToken(user.Id, user.Email, roles);
        var expiration = DateTime.UtcNow.AddMinutes(60);

        user.LastLoginAt = DateTime.UtcNow;
        await unitOfWork.SaveAsync(cancellationToken);

        var result = new TokenResponseDto(token, expiration);
        return Result<TokenResponseDto>.Success(result);
    }
}
