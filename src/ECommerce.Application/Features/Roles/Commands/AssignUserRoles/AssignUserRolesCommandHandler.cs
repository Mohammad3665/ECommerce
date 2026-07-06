using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Features.Roles.Commands.AssignUserRoles;

public class AssignUserRolesCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<AssignUserRolesCommand, Result>
{
    public async Task<Result> Handle(AssignUserRolesCommand request, CancellationToken cancellationToken)
    {
        var currentUserMaxLevel = currentUser.GetMaxRoleLevel();
        var hasSuperAdminInRequest = request.RoleSlugs.Any(slug => slug.Trim().ToLower() == "super-admin");
        if (hasSuperAdminInRequest && currentUserMaxLevel < 100)
        {
            var error = new Error(
                "Role.UnauthorizedAssignment",
                "شما سطح دسترسی لازم برای اعطای نقش مدیریت کل (Super Admin) را ندارید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        var cleanedSlugs = request.RoleSlugs.Select(s => s.Trim().ToLower()).ToList();
        var roles = await unitOfWork.RoleRepository.GetAllAsync(
            expression: r => cleanedSlugs.Contains(r.Slug.Trim()),
            cancellationToken: cancellationToken
        );
        if (roles.Count() != cleanedSlugs.Count())
        {
            var error = new Error(
                "Role.SomeRolesNotFound",
                "یک یا چند نقش ارسال شده در سیستم یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var targetMaxRoleLevel = roles.Max(r => r.Level);
        if (targetMaxRoleLevel >= currentUserMaxLevel && currentUserMaxLevel < 100)
        {
            var error = new Error(
                "Role.InvalidLevelAssignment",
                "شما نمی‌توانید نقشی هم‌سطح یا بالاتر از خودتان را به کاربری تخصیص دهید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        await unitOfWork.UserRoleRepository.DeleteUserRolesByUserIdAsync(request.UserId, cancellationToken);

        var newUserRoles = roles.Select(role => new UserRole
        {
            UserId = request.UserId,
            RoleId = role.Id,
            AssignedAt = DateTime.UtcNow,
            AssignedByUserId = currentUser.UserId,
        }).ToList();

        await unitOfWork.UserRoleRepository.AddRangeAsync(newUserRoles, cancellationToken);

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Role.AssignmentFailed",
                "خطایی در تخصیص نقش‌ها رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        return Result.Success();
    }

}
