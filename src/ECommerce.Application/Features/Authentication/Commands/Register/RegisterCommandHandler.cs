using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;
using Mapster;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.Entities.Application.Role;

namespace ECommerce.Application.Features.Authentication.Commands.Register;

public class RegisterUserCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService)
    : IRequestHandler<RegisterCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Email == request.Email,
            cancellationToken: cancellationToken
        );

        if (emailTaken is not null)
        {
            var error = new Error(
                "Auth.EmailTaken",
                "This email aleardy exists.", 
                ErrorType.Conflict
            );
            return Result<Guid>.Failure(error);
        }

        var roleName = request.Role ?? "Customer";
        var role = await unitOfWork.RoleRepository.GetAsync(
            expression: r => r.Name == roleName,
            cancellationToken: cancellationToken
        );
        if (role is null)
        {
            var error = new Error(
                "Auth.NotFound",
                $"{roleName} role not found in system", 
                ErrorType.NotFound
            );
            return Result<Guid>.Failure(error);
        }
        var user = request.Adapt<User>();
        user.Id = Guid.NewGuid();
        user.PasswordHash = passwordService.Hash(request.Password);
        user.UserRoles = new List<UserRole>
        {
            new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                AssignedAt = DateTime.UtcNow,
                AssignedByUserId = null
            }
        };
        // user.UserRoles = new List<Role> { role };
        user.IsActive = true;
        user.IsEmailConfirmed = request.Role != null;
        user.SecurityCode = null;
        user.SecurityCodeExpiry = null;

        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        return Result<Guid>.Success(user.Id);
    }

}
