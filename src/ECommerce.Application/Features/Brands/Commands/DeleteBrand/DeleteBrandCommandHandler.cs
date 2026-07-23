using ECommerce.Domain.Entities.Product.Events.Brand;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.Brands.Commands.DeleteBrand;

public class DeleteBrandCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBrandCommand, Result>
{
    public async Task<Result> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await unitOfWork.BrandRepository.GetAsync(
            expression: b => b.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (brand is null)
            return new Error("Brand.NotFound", "برند یافت نشد.", ErrorType.NotFound);

        var hasProduct = await unitOfWork.ProductRepository.IsExistAsync(
            expression: p => p.BrandId == brand.Id,
            cancellationToken: cancellationToken
        );
        if (hasProduct)
            return new Error("Brand.HasDependencies", "حذف این برند امکان‌پذیر نیست، زیرا محصولاتی در سیستم به این برند متصل هستند. ابتدا برند محصولات را عوض کنید.", ErrorType.Validation);

        var imageUrlToRemove = brand.LogoImageUrl;

        unitOfWork.BrandRepository.DeletePermanently(brand);

        brand.AddDomainEvent(new BrandDeletedDomainEvent(imageUrlToRemove));

        try
        {
            var saveResult = await unitOfWork.SaveAsync(cancellationToken);
            if (saveResult.IsFailure)
                return new Error("Brand.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);
        }
        catch (DbUpdateException)
        {
            return new Error("Brand.HasDependencies", "حذف این برند امکان‌پذیر نیست، زیرا محصولاتی در سیستم به این برند متصل هستند.", ErrorType.Validation);
        }

        return Result.Success();
    }
}
