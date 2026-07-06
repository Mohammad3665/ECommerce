using ECommerce.Application.Dtos.Brands;
using ECommerce.Application.Features.Brands.Commands.CreateBrand;
using ECommerce.Application.Features.Brands.Commands.DeleteBrand;
using ECommerce.Application.Features.Brands.Commands.EditBrand;
using ECommerce.Application.Features.Brands.Commands.ToggleBrandStatus;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class BrandsController(ISender sender, ILogger<BrandsController> logger, IFileService fileService) : AdminBaseController
{
    [HttpPost]
    [HasPermission("brands.create")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateBrandRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string relativeUrl = "";
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishName.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/categories");
        }

        var command = new CreateBrandCommand(
            Name: request.Name,
            EnglishName: request.EnglishName,
            Description: request.Description,
            LogoImageUrl: relativeUrl,
            IsActive: request.IsActive
        );
        
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("brands.update")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit(string slug, [FromForm] EditBrandRequestDto request, IFormFile imageFile, CancellationToken cancellationToken)
    {
        string relativeUrl = "";
        if (imageFile is not null)
        {
            string fileNameSeed = $"{request.EnglishName.Trim()}_gallery";
            relativeUrl = await fileService.SaveFileAsync(imageFile, fileNameSeed, "uploads/categories");
        }

        var command = new EditBrandCommand(
            Slug: slug,
            Name: request.Name,
            EnglishName: request.EnglishName,
            Description: request.Description,
            LogoImageUrl: relativeUrl
        );

        var result = await sender.Send(command, cancellationToken);
        if (result.IsFailure && !string.IsNullOrEmpty(relativeUrl))
        {
            fileService.DeleteFile(relativeUrl);
        }

        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission("brands.delete")]
    public async Task<IActionResult> DeleteAsync(string slug, CancellationToken cancellationToken)
    {
        var command = new DeleteBrandCommand(slug);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("brands.update")]
    public async Task<IActionResult> ToggleStatus(string slug, [FromBody] bool isActive, CancellationToken cancellationToken)
    {
        var command = new ToggleBrandStatusCommand(slug, isActive);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}