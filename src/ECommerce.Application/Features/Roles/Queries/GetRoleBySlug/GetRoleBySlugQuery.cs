using ECommerce.Application.Dtos.Roles;

namespace ECommerce.Application.Features.Roles.Queries.GetRoleBySlug;

public record GetRoleBySlugQuery(string Slug) : IRequest<Result<GetRoleResponseDto>>;
