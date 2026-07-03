using ECommerce.Application.Dtos.ArticleCategories;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.ArticleCategories.Queries.GetAllArticleCategories;

public record GetAllArticleCategoriesQuery : IRequest<Result<IEnumerable<GetArticleCategoryResponseDto>>>;
