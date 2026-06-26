using ECommerce.Api.Common.Extensions;
using ECommerce.Api.Controllers.v1;
using ECommerce.Application.Dtos.Roles;
using ECommerce.Application.Features.Roles.Commands.AssignUserRoles;
using ECommerce.Application.Features.Roles.Commands.CreateRole;
using ECommerce.Application.Features.Roles.Commands.DeleteRole;
using ECommerce.Application.Features.Roles.Commands.EditRole;
using ECommerce.Infrastructure.Identity.Attributes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

public class RolesController(ISender sender, ILogger<RolesController> logger) : AdminBaseController
{
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
        var command = new EditRoleCommand(slug, dto.DisplayName, dto.Description, dto.level, dto.PermissionIds);
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

    [HttpPost("Assign/{userId:Guid}")]
    [HasPermission("roles.update")]
    public async Task<IActionResult> AssignRoles(Guid userId, [FromBody] AssignUserRolesRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new AssignUserRolesCommand(userId, dto.RoleSlugs);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}