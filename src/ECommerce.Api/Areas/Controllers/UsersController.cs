using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Application.Dtos.Users;
using ECommerce.Application.Features.Authentication.Commands.CreateUserByAdmin;
using ECommerce.Application.Features.Authentication.Commands.Register;
using ECommerce.Application.Features.Users.Commands.ToggleUserStatus;
using ECommerce.Application.Features.Users.Queries.GetUsersList;
using ECommerce.Infrastructure.Identity.Attributes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

public class UsersController(ISender sender, ILogger<UsersController> logger) : AdminBaseController
{
    [HttpPost]
    [HasPermission("users.create")]
    public async Task<IActionResult> Create([FromBody] CreateUserByAdminRequestDto request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateUserByAdminCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet]
    [HasPermission("users.read")]
    public async Task<IActionResult> GetPaged([FromQuery] GetPagedUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("users.update")]
    public async Task<IActionResult> ToggleStatus([FromRoute] Guid id, [FromBody] bool isActive, CancellationToken cancellationToken)
    {
        var command = new ToggleUserStatusCommand(id, isActive);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}