using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.ResetForgotedPassword;

public record ResetForgotedPasswordCommand(
    string Email,
    string SecurityCode,
    string NewPassword
) : IRequest<Result>;
