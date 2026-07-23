namespace ECommerce.Application.Features.Permissions.Queries.GetAllPermissions;

public record GetAllPermissionsQuery : IRequest<Result<IEnumerable<PermissionDto>>>;
