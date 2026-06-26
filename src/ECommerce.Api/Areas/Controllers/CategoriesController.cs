using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Categories;
using ECommerce.Application.Features.Categories.Commands.CreateCategory;
using ECommerce.Application.Features.Categories.Commands.DeleteCategory;
using ECommerce.Application.Features.Categories.Commands.EditCategory;
using ECommerce.Application.Features.Categories.Commands.MoveCategory;
using ECommerce.Application.Features.Categories.Commands.ToggleCategoryStatus;
using ECommerce.Application.Features.Categories.Queries.GetPagedCategories;
using ECommerce.Infrastructure.Identity.Attributes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

public class CategoriesController(ILogger<CategoriesController> logger, ISender sender) : AdminBaseController
{
    [HttpGet]
    [HasPermission("categories.read")]
    public async Task<IActionResult> GetPaged([FromQuery] GetPagedCategoriesQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission("categories.create")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateCategoryCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("categories.update")]
    public async Task<IActionResult> Edit(string slug, [FromBody] EditCategoryDto dto, CancellationToken cancellationToken)
    {
        var command = new EditCategoryCommand(slug, dto.Name, dto.EnglishName, dto.Description, dto.ImageUrl);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission("categories.delete")]
    public async Task<IActionResult> Delete(string slug, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(slug);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("categories.update")]
    public async Task<IActionResult> ToggleStatus(string slug, [FromBody] bool IsActive, CancellationToken cancellationToken)
    {
        var command = new ToggleCategoryStatusCommand(slug, IsActive);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("categories.update")]
    public async Task<IActionResult> Move(string slug, [FromBody] MoveCategoryRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new MoveCategoryCommand(slug, dto.newParentId);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}