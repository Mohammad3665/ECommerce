using ECommerce.Application.Dtos.Roles;
using ECommerce.Application.Features.Roles.Commands.AssignUserRoles;
using ECommerce.Application.Features.Roles.Commands.CreateRole;
using ECommerce.Application.Features.Roles.Commands.DeleteRole;
using ECommerce.Application.Features.Roles.Commands.EditRole;
using ECommerce.Application.Features.Roles.Queries.GetAllRoles;
using ECommerce.Application.Features.Roles.Queries.GetRoleBySlug;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class RolesController(ISender sender, ILogger<RolesController> logger) : AdminBaseController
{

    [HttpGet]
    [HasPermission("roles.read")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllRolesQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{slug}")]
    [HasPermission("roles.read")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var query = new GetRoleBySlugQuery(slug);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission("roles.create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateRoleCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("roles.update")]
    public async Task<IActionResult> Edit(string slug, [FromBody] EditRoleRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new EditRoleCommand(slug, dto.DisplayName, dto.Description, dto.level, dto.GrantAllPermissions, dto.PermissionIds);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission("roles.delete")]
    public async Task<IActionResult> Delete(string slug, [FromQuery] bool forceDelete, CancellationToken cancellationToken)
    {
        var command = new DeleteRoleCommand(slug, forceDelete);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost("{userId:Guid}")]
    [HasPermission("roles.update")]
    public async Task<IActionResult> AssignRoles(Guid userId, [FromBody] AssignUserRolesRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new AssignUserRolesCommand(userId, dto.RoleSlugs);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}