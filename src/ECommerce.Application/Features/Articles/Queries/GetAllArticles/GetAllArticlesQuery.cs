using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Articles.Queries.GetAllArticles;

public record GetAllArticlesQuery : IRequest<Result<IEnumerable<GetAllArticlesResponseDto>>>;
