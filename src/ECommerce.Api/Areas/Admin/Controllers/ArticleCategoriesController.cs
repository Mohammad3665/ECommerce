using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.ArticleCategories;
using ECommerce.Application.Features.ArticleCategories.Commands.CreateArticleCategory;
using ECommerce.Application.Features.ArticleCategories.Commands.DeleteArticleCategory;
using ECommerce.Application.Features.ArticleCategories.Commands.EditArticleCategory;
using ECommerce.Infrastructure.Identity.Attributes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class ArticleCategoriesController(ISender sender, ILogger<ArticleCategoriesController> logger) : AdminBaseController
{
    [HttpPost]
    [HasPermission("articles.create")]
    public async Task<IActionResult> Create([FromBody] CreateArticleCategoryRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateArticleCategoryCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("articles.update")]
    public async Task<IActionResult> Edit(string slug, [FromBody] EditArticleCategoryRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new EditArticleCategoryCommand(slug, dto.Name, dto.EnglishName);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission("articles.delete")]
    public async Task<IActionResult> Delete(string slug, CancellationToken cancellationToken)
    {
        var command = new DeleteArticleCategoryCommand(slug);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}
