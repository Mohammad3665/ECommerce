namespace ECommerce.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<DeleteRoleCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.RoleRepository.GetAsync(
            expression: r => r.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (role is null)
            return new Error("Role.NotFound", "نقش مورد نظر یافت نشد.", ErrorType.NotFound);

        var defaultRoles = new[] { "super-admin", "admin", "content-manager", "customer" };
        if (role.IsSystemProtected || defaultRoles.Contains(role.Slug.ToLower()))
            return new Error("Role.SystemProtectedDeletion", "حذف نقش‌های پیش‌فرض و سیستمی برنامه کاملاً غیرمجاز است.", ErrorType.Validation);

        var currentUserMaxLevel = currentUser.GetMaxRoleLevel();
        if (role.Level >= currentUserMaxLevel && currentUserMaxLevel < 100)
            return new Error("Role.InvalidLevel", "شما دسترسی لازم برای حذف نقشی با این سطح یا بالاتر را ندارید.", ErrorType.Validation);

        var hasAssignedUsers = await unitOfWork.UserRoleRepository.HasAssignedUsersAsync(role.Id, cancellationToken);
        if (hasAssignedUsers)
        {
            if (!request.ForceDelete)
                return new Error("Role.HasAssignedUsers", "این نقش به کاربران سیستم اختصاص داده شده است. آیا از حذف آن و انتقال کاربران به نقش مشتری مطمئن هستید؟", ErrorType.Validation);

            var customerRole = await unitOfWork.RoleRepository.GetAsync(
                expression: r => r.Slug == "customer",
                cancellationToken: cancellationToken
            );
            if (customerRole is null)
                return new Error("Role.CustomerRoleNotFound", "نقش پایه پیش‌فرض (Customer) در سیستم یافت نشد. انتقال کاربران امکان‌پذیر نیست.", ErrorType.Unexpected);

            await unitOfWork.UserRoleRepository.MigrateUsersToRoleAsync(role.Id, customerRole.Id, cancellationToken);
        }

        unitOfWork.RoleRepository.DeletePermanently(role);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Role.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
