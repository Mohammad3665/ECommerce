using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Application.Role;
using MediatR;

namespace ECommerce.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(
    string Name,
    string DisplayName,
    string Description,
    int Level,
    bool GrantAllPermissions,
    List<long>? PermissionIds) : IRequest<Result<long>>, IMapTo<Role>;
