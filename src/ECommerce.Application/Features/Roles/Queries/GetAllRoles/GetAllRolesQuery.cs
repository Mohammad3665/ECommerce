using ECommerce.Application.Dtos.Roles;

namespace ECommerce.Application.Features.Roles.Queries.GetAllRoles;

public record GetAllRolesQuery() : IRequest<Result<IEnumerable<GetAllRolesResponseDto>>>;
