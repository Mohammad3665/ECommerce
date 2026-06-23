using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(
    string Name,
    string DisplayName,
    string Description,
    int Level,
    bool GrantAllPermissions,
    List<long>? PermissionIds) : IRequest<Result<long>>;
