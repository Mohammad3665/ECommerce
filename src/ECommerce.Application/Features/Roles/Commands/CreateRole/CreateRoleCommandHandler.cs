using ECommerce.Application.Common.Extensions;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<CreateRoleCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var isDuplicate = await unitOfWork.RoleRepository.IsExistAsync(
            expression: r => r.Name.ToLower() == request.Name.ToLower(),
            cancellationToken: cancellationToken
        );
        if (isDuplicate)
        {
            var error = new Error(
                "Role.DuplicateName",
                "نقشی با این نام سیستمی قبلاً در سیستم ثبت شده است.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        var currentUserMaxLevel = currentUser.GetMaxRoleLevel();
        if (request.Level <= currentUserMaxLevel && currentUserMaxLevel < 100)
        {
            var error = new Error(
                "Role.InvalidLevel",
                "شما نمی‌توانید نقشی با سطح دسترسی بالاتر یا هم‌تراز با خود ایجاد کنید.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        if (request.GrantAllPermissions && currentUserMaxLevel < 100)
        {
            var error = new Error(
                "Role.UnauthorizedPermissionGrant",
                "فقط مدیران کل سیستم می‌توانند دسترسی کامل به یک نقش اعطا کنند.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        var newRole = request.Adapt<Role>();
        List<long> finalPermissionIds;
        if (request.GrantAllPermissions)
        {
            finalPermissionIds = await unitOfWork.PerimssionRepository.GetAllIdsAsync(cancellationToken);
        }
        else
        {
            finalPermissionIds = request.PermissionIds ?? [];
        }

        foreach (var permissionId in finalPermissionIds)
        {
            newRole.RolePermissions.Add(new RolePermission
            {
                PermissionId = permissionId
            });
        }

        newRole.Slug = request.Name.ToSlug();

        await unitOfWork.RoleRepository.AddAsync(newRole);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Role.CreateFailed",
                "خطایی در ذخیره‌سازی نقش جدید رخ داد.",
                ErrorType.Unexpected
            );
            return Result<long>.Failure(error);
        }

        return Result<long>.Success(newRole.Id);
    }
}
