namespace ECommerce.Application.Features.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(string Slug, bool ForceDelete = false) : IRequest<Result>;
