using ECommerce.Application.Features.ArticleCategories.Queries.GetAllArticleCategories;
using ECommerce.Application.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;

namespace ECommerce.Api.Controllers.v1;

public class ArticleCategoriesController(ISender sender, ILogger<ArticleCategoriesController> logger) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllArticleCategoriesQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var query = new GetArticleCategoryBySlugQuery(slug);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }
}
