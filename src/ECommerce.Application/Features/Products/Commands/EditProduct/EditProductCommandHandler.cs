using ECommerce.Application.Common.Extensions;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.EditProduct;

public class EditProductCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<EditProductCommand, Result>
{
    public async Task<Result> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.Slug == request.CurrentSlug.Trim().ToLower(),
            cancellationToken: cancellationToken,
            query => query.Images
        );
        if (product is null)
        {
            var error = new Error(
                "Product.NotFound",
                "محصول مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        if (product.EnglishName.ToLower() != request.EnglishName.Trim().ToLower())
        {
            string baseSlug = request.EnglishName.ToSlug();
            string finalSlug = baseSlug;
            int version = 1;

            while (await unitOfWork.ProductRepository.IsExistAsync(p => p.Slug == finalSlug && p.Id != product.Id, cancellationToken))
            {
                finalSlug = $"{baseSlug}-{version}";
                version++;
            }
            product.Slug = finalSlug;
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<EditProductCommand, Product>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Slug)
            .Ignore(dest => dest.Images)
            .Ignore(dest => dest.Specifications);

        request.Adapt(product, config);
        product.Name = request.Name.Trim();
        product.EnglishName = request.EnglishName.Trim();

        var dbImages = await unitOfWork.ProductImageRepository.GetAllAsync(
            expression: img => img.ProductId == product.Id,
            cancellationToken: cancellationToken
        );

        var requestFileNames = request.Images
            .Select(img => Path.GetFileName(img.ImageUrl).ToLower().Trim())
            .ToHashSet();

        var imagesToDeleteFromDb = product.Images
            .Where(dbImg => !requestFileNames.Contains(Path.GetFileName(dbImg.ImageUrl).ToLower().Trim()))
            .ToList();

        var physicalPathsToDelete = product.Images
            .Where(dbImg => !requestFileNames.Contains(Path.GetFileName(dbImg.ImageUrl).ToLower().Trim()))
            .Select(img => img.ImageUrl.Trim().Replace("\\", "/").TrimStart('/'))
            .ToList();

        foreach (var img in imagesToDeleteFromDb)
        {
            product.Images.Remove(img);
            unitOfWork.ProductImageRepository.DeletePermanently(img);
            unitOfWork.ProductImageRepository.Update(img);
        }

        var currentDbFileNames = product.Images
            .Select(img => Path.GetFileName(img.ImageUrl).ToLower().Trim())
            .ToHashSet();

        var freshImages = new List<ProductImage>();
        foreach (var img in request.Images)
        {
            freshImages.Add(new ProductImage
            {
                ProductId = product.Id,
                ImageUrl = img.ImageUrl,
                IsMain = img.IsMain,
                DisplayOrder = img.DisplayOrder
            });
        }

        product.Images = freshImages;
        product.Specifications = null!; 
        unitOfWork.ProductRepository.Update(product);

        var incomingSpecs = request.Specifications.ToList();

        var dbSpecs = (await unitOfWork.ProductSpecificationRepository.GetAllAsync(
            expression: spec => spec.ProductId == product.Id,
            cancellationToken: cancellationToken
        )).ToList();

        var specsToRemove = dbSpecs
            .Where(dbSpec => !incomingSpecs.Any(s => s.Key.NormalizePersian == dbSpec.Key.NormalizePersian))
            .ToList();

        foreach (var specToRemove in specsToRemove)
        {
            unitOfWork.ProductSpecificationRepository.DeletePermanently(specToRemove);
        }

        foreach (var incomingSpec in incomingSpecs)
        {
            var existingSpec = dbSpecs.FirstOrDefault(p => p.Key.NormalizePersian() == incomingSpec.Key.NormalizePersian());

            if (existingSpec is not null)
            {
                existingSpec.Value = incomingSpec.Value.NormalizePersian();
                unitOfWork.ProductSpecificationRepository.Update(existingSpec);
            }
            else
            {
                var newSpec = new ProductSpecification
                {
                    ProductId = product.Id,
                    Key = incomingSpec.Key.NormalizePersian(),
                    Value = incomingSpec.Value.NormalizePersian()
                };
                await unitOfWork.ProductSpecificationRepository.AddAsync(newSpec, cancellationToken);
            }
        }

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Product.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        foreach (var relativeUrl in physicalPathsToDelete)
        {
            fileService.DeleteFile(relativeUrl);
        }

        return Result.Success();
    }
}
