using ECommerce.Application.Authorization;
using ECommerce.Application.Dtos.Categories;
using ECommerce.Application.Features.Categories.Commands.CreateCategory;
using ECommerce.Application.Features.Categories.Commands.DeleteCategory;
using ECommerce.Application.Features.Categories.Commands.EditCategory;
using ECommerce.Application.Features.Categories.Commands.MoveCategory;
using ECommerce.Application.Features.Categories.Commands.ToggleCategoryStatus;
using ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class CategoriesController(ILogger<CategoriesController> logger, ISender sender, IFileService fileService) : AdminBaseController
{
    [HttpGet]
    [HasPermission(Permissions.Categories.Read)]
    public async Task<IActionResult> GetPaged([FromQuery] GetPagedCategoriesQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission(Permissions.Categories.Create)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateCategoryRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string? relativeUrl = null;
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishName.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/categories");
        }

        var command = new CreateCategoryCommand(
            Name: request.Name,
            EnglishName: request.EnglishName,
            Description: request.Description,
            ImageUrl: relativeUrl,
            ParentCategoryId: request.ParentCategoryId
        );

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission(Permissions.Categories.Update)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit(string slug, [FromForm] EditCategoryRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string? relativeUrl = null;
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishName.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/categories");

        }

        var command = new EditCategoryCommand(
            Slug: slug,
            Name: request.Name,
            EnglishName: request.EnglishName,
            Description: request.Description,
            ImageUrl: relativeUrl
        );

        var result = await sender.Send(command, cancellationToken);
        if (result.IsFailure && !string.IsNullOrEmpty(relativeUrl))
        {
            fileService.DeleteFile(relativeUrl);
        }

        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission(Permissions.Categories.Delete)]
    public async Task<IActionResult> Delete(string slug, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(slug);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission(Permissions.Categories.Update)]
    public async Task<IActionResult> ToggleStatus(string slug, [FromBody] bool IsActive, CancellationToken cancellationToken)
    {
        var command = new ToggleCategoryStatusCommand(slug, IsActive);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission(Permissions.Categories.Update)]
    public async Task<IActionResult> Move(string slug, [FromBody] MoveCategoryRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new MoveCategoryCommand(slug, dto.newParentId);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}