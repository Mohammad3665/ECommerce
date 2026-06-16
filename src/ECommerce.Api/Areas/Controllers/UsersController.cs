using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Application.Features.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

[Route(template: "Api/Admin/[controller]/[action]")] 
public class UsersController(ISender sender, ILogger<UsersController> logger) : AdminBaseController
{
    public async Task<IActionResult> Create([FromBody] AdminCreateUserRequestDto request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(
            request.FullName, 
            request.Email, 
            request.PhoneNumber, 
            request.Password,
            request.Role
        );

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}