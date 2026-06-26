using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Categories;
using ECommerce.Application.Features.Categories.Commands.CreateCategory;
using ECommerce.Application.Features.Categories.Commands.DeleteCategory;
using ECommerce.Application.Features.Categories.Commands.EditCategory;
using ECommerce.Application.Features.Categories.Queries.GetPagedCategories;
using ECommerce.Domain.Specifications.Common;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ECommerce.Api.Areas.Controllers;

public class CategoriesController(ILogger<CategoriesController> logger, ISender sender) : AdminBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] GetPagedCategoriesQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateCategoryCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    public async Task<IActionResult> Edit(string slug, [FromBody] EditCategoryDto dto, CancellationToken cancellationToken)
    {
        var command = new EditCategoryCommand(slug, dto.Name, dto.EnglishName, dto.Description, dto.ImageUrl);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}