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
        {
            var error = new Error(
                "Brand.DuplicateName",
                "برند با این نام انگلیسی قبلا در سیستم ثبت شده است.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        var brand = request.Adapt<Brand>();

        await unitOfWork.BrandRepository.AddAsync(brand, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Brand.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result<long>.Failure(error);
        }

        return Result<long>.Success(brand.Id);
    }
}
