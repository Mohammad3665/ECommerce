namespace ECommerce.Application.Dtos.Roles;

public record GetRoleResponseDto(
    string DisplayName,
    string Description,
    int level,
    List<long>? PermissionIds
);