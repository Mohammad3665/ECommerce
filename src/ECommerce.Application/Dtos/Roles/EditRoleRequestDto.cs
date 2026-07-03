namespace ECommerce.Application.Dtos.Roles;

public record EditRoleRequestDto(
    string DisplayName,
    string Description,
    int level,
    bool GrantAllPermissions,
    List<long> PermissionIds
);
