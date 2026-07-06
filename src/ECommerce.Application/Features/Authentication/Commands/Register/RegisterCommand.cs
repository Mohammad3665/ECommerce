using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Features.Authentication.Commands.Register;

public record RegisterCommand(
    string FullName,
    string Email,
    string PhoneNumber,
    string Password,
    string? Role = null
) : IRequest<Result<Guid>>, IMapTo<User>;
