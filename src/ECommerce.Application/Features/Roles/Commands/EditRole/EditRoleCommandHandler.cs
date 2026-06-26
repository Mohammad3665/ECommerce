using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<EditRoleCommand, Result>
{
    public async Task<Result> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.RoleRepository.GetAsync(
            expression: r => r.Slug == request.Slug.Trim().ToLower(),
            includes: r => r.RolePermissions,
            cancellationToken: cancellationToken);
        if (role is null)
        {
            var error = new Error(
                "Role.NotFound",
                "نقش مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }
        if (role.IsSystemProtected || role.Slug == "super-admin")
        {
            var error = new Error(
                "Role.SystemProtected",
                "نقش‌های سیستمی و محافظت‌شده قابل ویرایش یا تغییر دسترسی نیستند.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        var currentUserMaxLevel = currentUser.GetMaxRoleLevel();
        if (role.Level >= currentUserMaxLevel && currentUserMaxLevel < 100)
        {
            var error = new Error(
                "Role.InvalidLevel",
                "شما دسترسی لازم برای ویرایش نقشی با این سطح یا بالاتر را ندارید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        role.DisplayName = request.DisplayName;
        role.Description = request.Description;

        if (request.PermissionIds is not null)
        {
            var currentPermissionIds = role.RolePermissions.Select(rp => rp.PermissionId).ToList();
            var newPermissionIds = request.PermissionIds;

            var permissionsToRemove = role.RolePermissions.Where(rp => !newPermissionIds.Contains(rp.PermissionId)).ToList();
            foreach (var rp in permissionsToRemove)
            {
                role.RolePermissions.Remove(rp);
            }

            var permissionsToAdd = newPermissionIds
                .Where(id => !currentPermissionIds.Contains(id))
                .Select(id => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = id
                }
            );
            foreach (var newRp in permissionsToAdd)
            {
                role.RolePermissions.Add(newRp);
            }
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<EditRoleCommand, Role>()
            .IgnoreNullValues(true);
        request.Adapt(role, config);

        unitOfWork.RoleRepository.Update(role);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
