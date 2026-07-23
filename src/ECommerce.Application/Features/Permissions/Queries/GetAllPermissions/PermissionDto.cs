using ECommerce.Domain.Entities.Application.Role;

namespace ECommerce.Application.Features.Permissions.Queries.GetAllPermissions;

public record PermissionDto(
    long Id,
    string Name,
    string Module,
    string Description
) : ECommerce.Application.Common.Mapping.IMapFrom<Permission>;
