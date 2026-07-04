using ECommerce.Api.Areas.Admin.Controllers;
using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Features.Articles.Queries.GetAllArticles;
using ECommerce.Application.Features.Articles.Queries.GetArticleBySlug;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.v1;

public class ArticlesController(ISender sender, ILogger<ArticlesController> logger) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllArticlesQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var query = new GetArticleBySlugQuery(slug);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }
}
