using ECommerce.Domain.Entities.Application.Role;

namespace ECommerce.Application.Features.Permissions.Queries.GetAllPermissions;

public class GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPermissionsQuery, Result<IEnumerable<PermissionDto>>>
{
    public async Task<Result<IEnumerable<PermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await unitOfWork.PerimssionRepository.GetAllAsync<PermissionDto>(
            expression: null,
            order: query => query.OrderBy(p => p.Module).ThenBy(p => p.Name),
            cancellationToken: cancellationToken
        );

        if (permissions is null || !permissions.Any())
            return new Error("Permission.NotFound", "دسترسی‌ای یافت نشد.", ErrorType.NotFound);

        return Result<IEnumerable<PermissionDto>>.Success(permissions);
    }
}
