using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<GetUserResponseDto>>;
