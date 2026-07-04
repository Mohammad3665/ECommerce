using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Articles.Queries.GetArticleBySlug;

public record GetArticleBySlugQuery(string Slug) : IRequest<Result<GetArticleResponseDto>>;
