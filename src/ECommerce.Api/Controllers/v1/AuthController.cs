using System.Security.Claims;
using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Authentication;
using ECommerce.Application.Features.Authentication.Commands.ConfirmEmail;
using ECommerce.Application.Features.Authentication.Commands.ForgotPassword;
using ECommerce.Application.Features.Authentication.Commands.Logout;
using ECommerce.Application.Features.Authentication.Commands.RefreshToken;
using ECommerce.Application.Features.Authentication.Commands.Register;
using ECommerce.Application.Features.Authentication.Commands.ResendVerificationEmailCommand;
using ECommerce.Application.Features.Authentication.Commands.ResetForgotedPassword;
using ECommerce.Application.Features.Authentication.Commands.ResetPassword;
using ECommerce.Application.Features.Authentication.Queries.Login;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.v1;

public class AuthController(ISender sender, ILogger<AuthController> logger) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<RegisterCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken cancellationToken)
    {
        var query = dto.Adapt<LoginQuery>();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<RefreshTokenCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<ForgotPasswordCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<ResetPasswordCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> ResetForgotedPassword([FromBody] ResetForgotedPasswordRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<ResetForgotedPasswordCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            return Unauthorized();
        }
        var command = new LogoutCommand(userId);

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<ConfirmEmailCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> ResendEmail([FromBody] ResendVerificationEmailRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<ResendVerificationEmailCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}