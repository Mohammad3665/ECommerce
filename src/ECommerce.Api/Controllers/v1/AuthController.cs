using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Application.Features.Authentication.Commands;
using ECommerce.Application.Features.Authentication.Commands.Register;
using ECommerce.Application.Features.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.v1;

[Route(template: "Api/V{V:apiVersion}/[controller]/[action]")]
public class AuthController(ISender sender, ILogger<AuthController> logger) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(
            request.FullName, 
            request.Email, 
            request.PhoneNumber, 
            request.Password
        );

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromQuery] LoginQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }
}