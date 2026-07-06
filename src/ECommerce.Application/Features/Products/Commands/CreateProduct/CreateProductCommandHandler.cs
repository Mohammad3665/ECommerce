using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Product;
using Mapster;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await unitOfWork.CategoryRepository.IsExistAsync(
            expression: c => c.Id == request.CategoryId,
            cancellationToken: cancellationToken
        );
        if (!categoryExists)
        {
            var error = new Error(
                "Product.CategoryNotFound",
                "دسته‌بندی انتخاب شده در سیستم یافت نشد.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        var brandExists = await unitOfWork.BrandRepository.IsExistAsync(
            expression: b => b.Id == request.BrandId,
            cancellationToken: cancellationToken
        );
        if (!brandExists)
        {
            var error = new Error(
                "Product.BrandNotFound",
                "برند انتخاب شده در سیستم یافت نشد.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        var isNameDuplicate = await unitOfWork.ProductRepository.IsExistAsync(
            expression: p => p.Name.ToLower() == request.Name.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isNameDuplicate)
        {
            var error = new Error(
                "Product.DuplicateName",
                "محصولی با این نام قبلاً ثبت شده است.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        string baseSlug = request.EnglishName.ToSlug();
        string finalSlug = baseSlug;
        int version = 1;

        while (await unitOfWork.ProductRepository.IsExistAsync(expression: p => p.Slug == finalSlug, cancellationToken: cancellationToken))
        {
            finalSlug = $"{baseSlug}-{version}";
            version++;
        }

        var product = request.Adapt<Product>();

        Console.WriteLine($"Mapped specs count: {product.Specifications.Count}, incoming: {request.Specifications.Count}");

        product.Slug = finalSlug;
        product.Name = request.Name.Trim();
        product.EnglishName = request.EnglishName.Trim();
        product.ViewCount = 0;

        product.Specifications = request.Specifications
            .Select(s => new ProductSpecification
            {
                Key = s.Key.NormalizePersian(),
                Value = s.Value.NormalizePersian()
            })
            .ToList();

        product.Images = request.Images
            .Select(img => new ProductImage
            {
                ImageUrl = img.ImageUrl,
                IsMain = img.IsMain,
                DisplayOrder = img.DisplayOrder
            })
            .ToList();

        await unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Product.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result<long>.Failure(error);
        }

        return Result<long>.Success(product.Id);
    }
}