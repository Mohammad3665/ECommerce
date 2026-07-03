using ECommerce.Application.Dtos.Roles;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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

        return Result<IEnumerable<GetAllRolesResponseDto>>.Success(roles);
    }
}