using ECommerce.Application.Dtos.Articles;

namespace ECommerce.Application.Features.Articles.Queries.GetAllArticles;

public record GetAllArticlesQuery : IRequest<Result<IEnumerable<GetAllArticlesResponseDto>>>;
