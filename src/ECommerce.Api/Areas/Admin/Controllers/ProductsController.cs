using System.Text.Json;
using ECommerce.Application.Authorization;
using ECommerce.Application.Dtos.Products;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.DeleteProduct;
using ECommerce.Application.Features.Products.Commands.EditProduct;
using ECommerce.Application.Features.Products.Queries.GetLowStockProducts;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class ProductsController(ISender sender, ILogger<ProductsController> logger, IFileService fileService) : AdminBaseController
{

    [HttpGet]
    [HasPermission(Permissions.Products.Read)]
    public async Task<IActionResult> GetLowStockAlerts(CancellationToken cancellationToken)
    {
        var query = new GetLowStockProductsQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission(Permissions.Products.Create)]
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

        var specifications = new List<SpecificationDto>();
        if (!string.IsNullOrEmpty(request.SpecificationsJson))
        {
            specifications = JsonSerializer.Deserialize<List<SpecificationDto>>(
                request.SpecificationsJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ?? new List<SpecificationDto>();
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
            Specifications: specifications,
            Images: uploadedImages
        );
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{slug}")]
    [HasPermission(Permissions.Products.Update)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Edit([FromRoute] string slug, [FromForm] EditProductRequestDto request, List<IFormFile> newImageFiles, CancellationToken cancellationToken)
    {

        var finalImages = new List<ProductImageDto>();

        if (!string.IsNullOrEmpty(request.ImagesJson))
        {
            var existingImages = JsonSerializer.Deserialize<List<ExistingImageDto>>(request.ImagesJson,
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
                string fileNameSeed = $"{request.EnglishName.Trim()}_gallery";
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

        var specifications = new List<SpecificationDto>();
        if (!string.IsNullOrEmpty(request.SpecificationsJson))
        {
            specifications = JsonSerializer.Deserialize<List<SpecificationDto>>(
                request.SpecificationsJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ?? new List<SpecificationDto>();
        }

        var command = new EditProductCommand(
            CurrentSlug: slug,
            Name: request.Name,
            EnglishName: request.EnglishName,
            Description: request.Description,
            ShortDescription: request.ShortDescription,
            Price: request.Price,
            StockQuantity: request.StockQuantity,
            BrandId: request.BrandId,
            CategoryId: request.CategoryId,
            Specifications: specifications,
            Images: finalImages
        );

        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{slug}")]
    [HasPermission(Permissions.Products.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string slug, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand(slug);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}