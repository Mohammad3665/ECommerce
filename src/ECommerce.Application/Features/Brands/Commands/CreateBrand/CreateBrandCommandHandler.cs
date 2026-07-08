using ECommerce.Domain.Entities.Product;
using Mapster;

namespace ECommerce.Application.Features.Brands.Commands.CreateBrand;

public class CreateBrandCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateBrandCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var isDuplicateEnglishName = await unitOfWork.BrandRepository.IsExistAsync(
            expression: b => b.EnglishName.ToLower() == request.EnglishName.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isDuplicateEnglishName)
            return new Error("Brand.DuplicateName", "برند با این نام انگلیسی قبلا در سیستم ثبت شده است.", ErrorType.Validation);

        var brand = request.Adapt<Brand>();

        await unitOfWork.BrandRepository.AddAsync(brand, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Brand.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            brand.Id;
    }
}
