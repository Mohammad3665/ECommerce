using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<Result<IEnumerable<GetAllUsersResponseDto>>>;
