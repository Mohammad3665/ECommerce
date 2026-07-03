using ECommerce.Application.Dtos.ArticleCategories;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;

public record GetArticleCategoryBySlugQuery(string Slug) : IRequest<Result<GetArticleCategoryResponseDto>>;
