namespace ECommerce.Application.Dtos.Roles;

public record EditRoleRequestDto(
    string DisplayName,
    string Description,
    int? level,
    List<long>? PermissionIds
);
