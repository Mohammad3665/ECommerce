using ECommerce.Application.Dtos.Roles;

namespace ECommerce.Application.Features.Roles.Queries.GetAllRoles;

public class GetAllRolesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllRolesQuery, Result<IEnumerable<GetAllRolesResponseDto>>>
{
    public async Task<Result<IEnumerable<GetAllRolesResponseDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await unitOfWork.RoleRepository.GetAllAsync<GetAllRolesResponseDto>(
            expression: null,
            order: query => query.OrderBy(r => r.Name),
            cancellationToken: cancellationToken,
            includes: r => r.RolePermissions
        );
        if (roles is null || !roles.Any())
            return new Error("Role.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<GetAllRolesResponseDto>>.Success(roles);
    }
}
