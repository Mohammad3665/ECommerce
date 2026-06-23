namespace ECommerce.Application.Dtos.Roles;

public record CreateRoleRequestDto(
    string Name,
    string DisplayName,
    string Description,
    int Level,
    bool GrantAllPermissions,
    List<long>? PermissionIds
);
