using System.Text.Json;
using ECommerce.Api.Common.Extensions;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Products;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.DeleteProduct;
using ECommerce.Application.Features.Products.Commands.EditProduct;
using ECommerce.Infrastructure.Identity.Attributes;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class ProductsController(ISender sender, ILogger<ProductsController> logger, IFileService fileService) : AdminBaseController
{
    [HttpPost]
    [HasPermission("products.create")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateProductRequestDto request, List<IFormFile> imageFiles, CancellationToken cancellationToken)
    {
        var uploadedImages = new List<ProductImageDto>();

        if (imageFiles is not null && imageFiles.Any())
        {
            int orderCounter = 1;
            foreach (var file in imageFiles)
            {
                string fileNameSeed = $"{request.EnglishName.Trim()}_gallery";
                string relativeUrl = await fileService.SaveFileAsync(file, fileNameSeed, "uploads/products");

                if (!string.IsNullOrEmpty(relativeUrl))
                {
                    var productImage = new ProductImageDto(
                        ImageUrl: relativeUrl,
                        IsMain: orderCounter == 1,
                        DisplayOrder: orderCounter++
                    );
                    uploadedImages.Add(productImage);
                }
            }
        }

        var command = new CreateProductCommand(
            Name: request.Name,
            EnglishName: request.EnglishName,
            Description: request.Description,
            ShortDescription: request.ShortDescription,
            Price: request.Price,
            StockQuantity: request.StockQuantity,
            BrandId: request.BrandId,
            CategoryId: request.CategoryId,
            Specifications: request.Specifications ?? new List<SpecificationDto>(),
            Images: uploadedImages
        );
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission("products.update")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit([FromRoute] string slug, [FromForm] EditProductRequestDto dto, List<IFormFile> newImageFiles, CancellationToken cancellationToken)
    {

        var finalImages = new List<ProductImageDto>();

        if (!string.IsNullOrEmpty(dto.ImagesJson))
        {
            var existingImages = JsonSerializer.Deserialize<List<ExistingImageDto>>(dto.ImagesJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (existingImages != null)
            {
                finalImages = existingImages.Select(img => new ProductImageDto(img.ImageUrl, img.IsMain, img.DisplayOrder)).ToList();
            }
        }

        if (newImageFiles is not null && newImageFiles.Any())
        {
            int nextOrder = finalImages.Any() ? finalImages.Max(img => img.DisplayOrder) + 1 : 1;

            foreach (var file in newImageFiles)
            {
                string fileNameSeed = $"{dto.EnglishName.Trim()}_gallery";
                string relativeUrl = await fileService.SaveFileAsync(file, fileNameSeed, "uploads/products");

                if (!string.IsNullOrEmpty(relativeUrl))
                {
                    var newProductImage = new ProductImageDto(
                        ImageUrl: relativeUrl,
                        IsMain: finalImages.Count() == 0,
                        DisplayOrder: nextOrder++
                    );
                    finalImages.Add(newProductImage);
                }
            }

        }

        var command = new EditProductCommand(
            CurrentSlug: slug,
            Name: dto.Name,
            EnglishName: dto.EnglishName,
            Description: dto.Description,
            ShortDescription: dto.ShortDescription,
            Price: dto.Price,
            StockQuantity: dto.StockQuantity,
            BrandId: dto.BrandId,
            CategoryId: dto.CategoryId,
            Specifications: dto.Specifications,
            Images: finalImages
        );

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission("products.delete")]
    public async Task<IActionResult> Delete([FromRoute] string slug, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(slug);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}