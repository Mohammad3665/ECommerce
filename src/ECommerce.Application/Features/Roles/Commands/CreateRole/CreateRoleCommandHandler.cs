using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Application.Role;
using Mapster;

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
            return new Error("Role.DuplicateName", "نقشی با این نام سیستمی قبلاً در سیستم ثبت شده است.", ErrorType.Validation);

        var currentUserMaxLevel = currentUser.GetMaxRoleLevel();
        if (request.Level <= currentUserMaxLevel && currentUserMaxLevel < 100)
            return new Error("Role.InvalidLevel", "شما نمی‌توانید نقشی با سطح دسترسی بالاتر یا هم‌تراز با خود ایجاد کنید.", ErrorType.Validation);

        if (request.GrantAllPermissions && currentUserMaxLevel < 100)
            return new Error("Role.UnauthorizedPermissionGrant", "فقط مدیران کل سیستم می‌توانند دسترسی کامل به یک نقش اعطا کنند.", ErrorType.Validation);

        var newRole = request.Adapt<Role>();

        List<long> finalPermissionIds;

        if (request.GrantAllPermissions)
            finalPermissionIds = await unitOfWork.PerimssionRepository.GetAllIdsAsync(cancellationToken);
        else
            finalPermissionIds = request.PermissionIds ?? [];

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

        return saveResult.IsFailure ?
            new Error("Role.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            newRole.Id;
    }
}
