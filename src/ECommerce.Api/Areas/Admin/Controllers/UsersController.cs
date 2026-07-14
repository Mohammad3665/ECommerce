using ECommerce.Application.Authorization;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Application.Features.Authentication.Commands.CreateUserByAdmin;
using ECommerce.Application.Features.Users.Commands.ToggleUserStatus;
using ECommerce.Application.Features.Users.Queries.GetAllUsers;
using ECommerce.Application.Features.Users.Queries.GetPagedUsers;
using ECommerce.Application.Features.Users.Queries.GetUserById;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class UsersController(ISender sender, ILogger<UsersController> logger) : AdminBaseController
{
    [HttpGet]
    [HasPermission(Permissions.Users.Read)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{id:Guid}")]
    [HasPermission(Permissions.Users.Read)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission(Permissions.Users.Create)]
    public async Task<IActionResult> Create([FromBody] CreateUserByAdminRequestDto request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateUserByAdminCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet]
    [HasPermission(Permissions.Users.Read)]
    public async Task<IActionResult> GetPaged([FromQuery] GetPagedUsersQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Users.Update)]
    public async Task<IActionResult> ToggleStatus([FromRoute] Guid id, [FromBody] bool isActive, CancellationToken cancellationToken)
    {
        var command = new ToggleUserStatusCommand(id, isActive);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}