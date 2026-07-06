using ECommerce.Application.Dtos.Users;

namespace ECommerce.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<GetUserResponseDto>>;
