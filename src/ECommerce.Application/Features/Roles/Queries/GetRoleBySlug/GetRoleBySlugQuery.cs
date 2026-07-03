using ECommerce.Application.Dtos.Roles;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Roles.Queries.GetRoleBySlug;

public record GetRoleBySlugQuery(string Slug) : IRequest<Result<GetRoleResponseDto>>;
