using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Roles.Commands.EditRole;

public record EditRoleCommand(
    string Slug,
    string DisplayName,
    string Description,
    int? Level,
    List<long>? PermissionIds
) : IRequest<Result>;
