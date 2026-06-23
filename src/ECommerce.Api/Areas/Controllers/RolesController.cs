using ECommerce.Api.Common.Extensions;
using ECommerce.Api.Controllers.v1;
using ECommerce.Application.Dtos.Roles;
using ECommerce.Application.Features.Roles.Commands.CreateRole;
using ECommerce.Infrastructure.Identity.Attributes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

public class RolesController(ISender sender, ILogger<RolesController> logger) : AdminBaseController
{
    [HttpPost]
    // [HasPermission("roles.create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateRoleCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}