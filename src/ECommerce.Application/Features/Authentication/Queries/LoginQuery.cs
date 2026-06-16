using ECommerce.Application.Dtos.Authentication;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Queries;

public record LoginQuery(string Email, string Password) : IRequest<Result<TokenResponseDto>>;
