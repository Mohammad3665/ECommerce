using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Features.Categories.Queries.GetAllCategories;
using ECommerce.Application.Features.Categories.Queries.GetCategoryById;
using ECommerce.Application.Features.Categories.Queries.GetCategoryBySlug;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.v1;

[Route("Api/V1/[controller]")]
public class CategoriesController(ISender sender, ILogger<CategoriesController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCategoriesQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var query = new GetCategoryBySlugQuery(slug);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }
}