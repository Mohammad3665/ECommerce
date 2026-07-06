using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Features.Authentication.Commands.CreateUserByAdmin;

public record CreateUserByAdminCommand(
    string FullName,
    string Email,
    string PhoneNumber,
    string? Password,
    string Role,
    Guid AdminId
) : IRequest<Result<Guid>>, IMapTo<User>;
