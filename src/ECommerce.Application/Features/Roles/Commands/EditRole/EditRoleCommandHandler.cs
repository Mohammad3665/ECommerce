using ECommerce.Domain.Entities.Application.Role;
using Mapster;

namespace ECommerce.Application.Features.Roles.Commands.EditRole;

public class EditRoleCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<EditRoleCommand, Result>
{
    public async Task<Result> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.RoleRepository.GetAsync(
            expression: r => r.Slug == request.Slug.Trim().ToLower(),
            includes: r => r.RolePermissions,
            cancellationToken: cancellationToken
        );
        if (role is null)
            return new Error("Role.NotFound", "نقش مورد نظر یافت نشد.", ErrorType.NotFound);

        if (role.IsSystemProtected || role.Slug == "super-admin")
            return new Error("Role.SystemProtected", "نقش‌های سیستمی و محافظت‌شده قابل ویرایش یا تغییر دسترسی نیستند.", ErrorType.Validation);

        var currentUserMaxLevel = currentUser.GetMaxRoleLevel();
        if (role.Level >= currentUserMaxLevel && currentUserMaxLevel < 100)
            return new Error("Role.InvalidLevel", "شما دسترسی لازم برای ویرایش نقشی با این سطح یا بالاتر را ندارید.", ErrorType.Validation);

        if (request.GrantAllPermissions && currentUserMaxLevel < 100)
            return new Error("Role.UnauthorizedPermissionGrant", "فقط مدیران کل سیستم می‌توانند دسترسی کامل به یک نقش اعطا کنند.", ErrorType.Validation);

        role.DisplayName = request.DisplayName;
        role.Description = request.Description;

        List<long> finalPermissionIds;
        if (request.GrantAllPermissions)
            finalPermissionIds = await unitOfWork.PerimssionRepository.GetAllIdsAsync(cancellationToken);
        else
            finalPermissionIds = request.PermissionIds ?? [];

        var currentPermissionIds = role.RolePermissions.Select(rp => rp.PermissionId).ToList();

        var permissionsToRemove = role.RolePermissions
            .Where(rp => !finalPermissionIds.Contains(rp.PermissionId))
            .ToList();

        foreach (var rp in permissionsToRemove)
            role.RolePermissions.Remove(rp);

        var permissionsToAdd = finalPermissionIds
            .Where(id => !currentPermissionIds.Contains(id))
            .Select(id => new RolePermission
            {
                RoleId = role.Id,
                PermissionId = id
            })
            .ToList();

        foreach (var newRp in permissionsToAdd)
            role.RolePermissions.Add(newRp);

        var config = new TypeAdapterConfig();
        config.NewConfig<EditRoleCommand, Role>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.RolePermissions);

        request.Adapt(role, config);

        unitOfWork.RoleRepository.Update(role);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Role.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
