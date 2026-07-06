namespace ECommerce.Application.Features.Roles.Commands.EditRole;

public record EditRoleCommand(
    string Slug,
    string DisplayName,
    string Description,
    int Level,
    bool GrantAllPermissions,
    List<long>? PermissionIds
) : IRequest<Result>;