using ECommerce.Domain.Entities.Application.Role;

namespace ECommerce.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(
    string Name,
    string DisplayName,
    string Description,
    int Level,
    bool GrantAllPermissions,
    List<long>? PermissionIds) : IRequest<Result<long>>, IMapTo<Role>;
