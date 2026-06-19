using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Application.Features.Authentication.Commands.CreateUserByAdmin;
using ECommerce.Application.Features.Authentication.Commands.Register;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

public class UsersController(ISender sender, ILogger<UsersController> logger) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserByAdminRequestDto request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateUserByAdminCommand>();

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}