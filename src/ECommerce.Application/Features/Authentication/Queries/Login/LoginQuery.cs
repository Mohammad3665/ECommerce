using ECommerce.Application.Dtos.Authentication;

namespace ECommerce.Application.Features.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<Result<TokenResponseDto>>;
