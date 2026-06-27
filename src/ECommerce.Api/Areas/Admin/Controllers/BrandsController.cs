using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Dtos.Brands;
using ECommerce.Application.Features.Brands.Commands.CreateBrand;
using ECommerce.Application.Features.Brands.Commands.DeleteBrand;
using ECommerce.Application.Features.Brands.Commands.EditBrand;
using ECommerce.Application.Features.Brands.Commands.ToggleBrandStatus;
using ECommerce.Infrastructure.Identity.Attributes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class BrandsController(ISender sender, ILogger<BrandsController> logger) : AdminBaseController
{
    [HttpPost]
    [HasPermission("brands.create")]
    public async Task<IActionResult> Create([FromBody] CreateBrandRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateBrandCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("brands.update")]
    public async Task<IActionResult> Edit(string slug, [FromBody] EditBrandRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new EditBrandCommand(slug, dto.Name, dto.EnglishName, dto.Description, dto.LogoImageUrl);
        var result = await sender.Send(command, cancellationToken);
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