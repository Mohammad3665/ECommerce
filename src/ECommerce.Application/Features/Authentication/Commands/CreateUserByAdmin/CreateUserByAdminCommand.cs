using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Identity;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.CreateUserByAdmin;

public record CreateUserByAdminCommand(
    string FullName,
    string Email,
    string PhoneNumber,
    string? Password,
    string Role,
    Guid AdminId
) : IRequest<Result<Guid>>, IMapTo<User>;
