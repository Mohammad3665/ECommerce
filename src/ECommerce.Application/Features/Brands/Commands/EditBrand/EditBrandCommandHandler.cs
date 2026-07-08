using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Product;
using Mapster;

namespace ECommerce.Application.Features.Brands.Commands.EditBrand;

public class EditBrandCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<EditBrandCommand, Result>
{
    public async Task<Result> Handle(EditBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await unitOfWork.BrandRepository.GetAsync(
            expression: b => b.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (brand is null)
            return new Error("Brand.NotFound", "برند یافت نشد.", ErrorType.NotFound);

        string oldImagePath = brand.LogoImageUrl;
        var hasNewImage = request.LogoImageUrl != null;

        brand.Slug = request.EnglishName.ToSlug();
        var config = new TypeAdapterConfig();
        config.NewConfig<EditBrandCommand, Brand>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Slug)
            .Ignore(dest => dest.LogoImageUrl);

        request.Adapt(brand, config);

        if (hasNewImage)
            brand.LogoImageUrl = request.LogoImageUrl!;

        unitOfWork.BrandRepository.Update(brand);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        if (saveResult.IsFailure)
            return new Error("Brand.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);

        if (hasNewImage && !string.IsNullOrEmpty(oldImagePath))
            fileService.DeleteFile(oldImagePath);

        return Result.Success();
    }
}
