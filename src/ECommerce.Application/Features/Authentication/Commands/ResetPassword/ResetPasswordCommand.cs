using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Email, 
    string SecurityCode, 
    string NewPassword) : IRequest<Result>;
