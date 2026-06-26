using System.Linq.Expressions;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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
        {
            var error = new Error(
                "Role.NotFound",
                "نقش مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var defaultRoles = new[] { "super-admin", "admin", "content-manager", "customer" };
        if (role.IsSystemProtected || defaultRoles.Contains(role.Slug.ToLower()))
        {
            var error = new Error(
                "Role.SystemProtectedDeletion",
                "حذف نقش‌های پیش‌فرض و سیستمی برنامه کاملاً غیرمجاز است.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        var currentUserMaxLevel = currentUser.GetMaxRoleLevel();
        if (role.Level >= currentUserMaxLevel && currentUserMaxLevel < 100)
        {
            var error = new Error(
                "Role.InvalidLevel",
                "شما دسترسی لازم برای حذف نقشی با این سطح یا بالاتر را ندارید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        var hasAssignedUsers = await unitOfWork.UserRoleRepository.HasAssignedUsersAsync(role.Id, cancellationToken);
        if (hasAssignedUsers)
        {
            if (!request.ForceDelete)
            {
                var error = new Error(
                    "Role.HasAssignedUsers",
                    "این نقش به کاربران سیستم اختصاص داده شده است. آیا از حذف آن و انتقال کاربران به نقش مشتری مطمئن هستید؟",
                    ErrorType.Validation
                );
                return Result.Failure(error);
            }
            
            var customerRole = await unitOfWork.RoleRepository.GetAsync(
                expression: r => r.Slug == "customer",
                cancellationToken: cancellationToken
            );
            if (customerRole is null)
            {
                var error = new Error(
                    "Role.CustomerRoleNotFound",
                    "نقش پایه پیش‌فرض (Customer) در سیستم یافت نشد. انتقال کاربران امکان‌پذیر نیست.",
                    ErrorType.Unexpected
                );
                return Result.Failure(error);
            }

            await unitOfWork.UserRoleRepository.MigrateUsersToRoleAsync(role.Id, customerRole.Id, cancellationToken);
        }

        unitOfWork.RoleRepository.DeletePermanently(role);

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Role.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        return Result.Success();
    }
}
