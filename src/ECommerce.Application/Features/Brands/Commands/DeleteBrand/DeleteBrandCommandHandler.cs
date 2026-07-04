using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<DeleteBrandCommand, Result>
{
    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await unitOfWork.BrandRepository.GetAsync(
            expression: b => b.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (brand is null)
        {
            var error = new Error(
                "Brand.NotFound",
                "برند یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var hasProduct = await unitOfWork.ProductRepository.IsExistAsync(
            expression: p => p.BrandId == brand.Id,
            cancellationToken: cancellationToken
        );
        if (hasProduct)
        {
            var error = new Error(
                "Brand.HasDependencies",
                "حذف این برند امکان‌پذیر نیست، زیرا محصولاتی در سیستم به این برند متصل هستند. ابتدا برند محصولات را عوض کنید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        var imageUrlToRemove = brand.LogoImageUrl;

        unitOfWork.BrandRepository.DeletePermanently(brand);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);


        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Brand.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        if (saveResult.IsSuccess && !string.IsNullOrWhiteSpace(imageUrlToRemove))
        {
            fileService.DeleteFile(imageUrlToRemove);
        }
        return Result.Success();
    }
}