using ECommerce.Application.Dtos.Roles;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Roles.Queries.GetAllRoles;

public record GetAllRolesQuery() : IRequest<Result<IEnumerable<GetAllRolesResponseDto>>>;
