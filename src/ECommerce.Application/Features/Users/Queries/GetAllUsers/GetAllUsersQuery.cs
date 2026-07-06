using ECommerce.Application.Dtos.Users;

namespace ECommerce.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<Result<IEnumerable<GetAllUsersResponseDto>>>;
