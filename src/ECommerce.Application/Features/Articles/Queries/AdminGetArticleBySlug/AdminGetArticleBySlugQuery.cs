using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Articles.Queries.AdminGetArticleBySlug;

public record AdminGetArticleBySlugQuery(string Slug) : IRequest<Result<GetAdminArticleResponseDto>>;
