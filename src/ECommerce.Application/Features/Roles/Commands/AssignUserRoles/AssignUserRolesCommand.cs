using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Roles.Commands.AssignUserRoles;

public record AssignUserRolesCommand(Guid UserId, List<string> RoleSlugs) : IRequest<Result>;
