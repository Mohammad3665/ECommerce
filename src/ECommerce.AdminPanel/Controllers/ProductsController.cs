using ECommerce.AdminPanel.Models;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Products;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.EditProduct;
using ECommerce.Application.Features.Products.Commands.DeleteProduct;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Application.Features.Products.Queries.GetProductBySlug;
using ECommerce.Application.Features.Brands.Queries.GetAllBrands;
using ECommerce.Application.Features.Categories.Queries.GetAllCategories;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class ProductsController(ISender sender, IFileService fileService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "لیست محصولات";
        var result = await sender.Send(new GetAllProductsQuery(), ct);
        var products = result.IsSuccess ? result.Data.Adapt<List<ProductListViewModel>>() : [];
        return View(products);
    }

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        ViewData["Title"] = "افزودن محصول";
        await LoadDropdowns(ct);
        return View(new ProductFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "افزودن محصول";
            await LoadDropdowns(ct);
            return View(model);
        }

        var images = new List<ProductImageDto>();
        var files = model.ImageFiles ?? (model.ImageFile is not null ? [model.ImageFile] : []);

        foreach (var file in files.Where(f => f is not null))
        {
            var url = await fileService.SaveFileAsync(file, model.EnglishName, "uploads/products");
            if (!string.IsNullOrEmpty(url))
                images.Add(new ProductImageDto(url, images.Count == 0, images.Count));
        }

        var specifications = model.Specifications ?? [];

        var command = new CreateProductCommand(
            model.Name, model.EnglishName, model.Description, model.ShortDescription,
            model.Price, model.StockQuantity, model.BrandId, model.CategoryId,
            specifications, images);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "افزودن محصول";
            await LoadDropdowns(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string slug, CancellationToken ct)
    {
        ViewData["Title"] = "ویرایش محصول";
        var result = await sender.Send(new GetProductBySlugQuery(slug), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data;
        var model = new ProductFormViewModel
        {
            Id = dto!.Id,
            Name = dto.Name,
            EnglishName = dto.EnglishName,
            Slug = dto.Slug,
            Description = dto.Description,
            ShortDescription = dto.ShortDescription,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            BrandName = dto.BrandName,
            CategoryName = dto.CategoryName,
            ExistingImageUrl = dto.Images.FirstOrDefault(i => i.IsMain)?.ImageUrl,
            Specifications = dto.ProductSpecifications,
            Images = dto.Images
        };

        await LoadDropdowns(ct);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "ویرایش محصول";
            await LoadDropdowns(ct);
            return View(model);
        }

        var images = new List<ProductImageDto>();

        // Keep existing images
        if (model.Images?.Any() == true)
        {
            images.AddRange(model.Images);
        }

        // Add new images if uploaded
        var files = model.ImageFiles ?? (model.ImageFile is not null ? [model.ImageFile] : []);
        foreach (var file in files.Where(f => f is not null))
        {
            var url = await fileService.SaveFileAsync(file, model.EnglishName, "uploads/products");
            if (!string.IsNullOrEmpty(url))
            {
                images.Add(new ProductImageDto(url, images.Count == 0, images.Count));
            }
        }

        var specifications = model.Specifications ?? [];

        var command = new EditProductCommand(
            model.Slug, model.Name, model.EnglishName, model.Description, model.ShortDescription,
            model.Price, model.StockQuantity, model.BrandId, model.CategoryId,
            specifications, images);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "خطا در ذخیره‌سازی");
            ViewData["Title"] = "ویرایش محصول";
            await LoadDropdowns(ct);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string slug, CancellationToken ct)
    {
        var command = new DeleteProductCommand(slug);
        var result = await sender.Send(command, ct);

        if (result.IsFailure)
            TempData["Error"] = result.Error.Description ?? "خطا در حذف محصول";

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdowns(CancellationToken ct)
    {
        var brandsResult = await sender.Send(new GetAllBrandsQuery(), ct);
        var categoriesResult = await sender.Send(new GetAllCategoriesQuery(), ct);

        ViewBag.Brands = brandsResult.IsSuccess
            ? brandsResult.Data.Select(b => new { b.Id, b.Name }).ToList()
            : [];
        ViewBag.Categories = categoriesResult.IsSuccess
            ? categoriesResult.Data.Select(c => new { c.Id, c.Name }).ToList()
            : [];
    }
}
