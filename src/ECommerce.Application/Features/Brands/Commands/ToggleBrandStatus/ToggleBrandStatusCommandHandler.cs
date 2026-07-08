using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Features.Brands.Commands.ToggleBrandStatus;

public class ToggleBrandStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleBrandStatusCommand, Result>
{
    public async Task<Result> Handle(ToggleBrandStatusCommand request, CancellationToken cancellationToken)
    {
        var brand = await unitOfWork.BrandRepository.GetAsync(
            expression: b => b.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (brand is null)
            return new Error("Brand.NotFound", "برند یافت نشد.", ErrorType.NotFound);

        brand.IsActive = request.IsActive;

        unitOfWork.BrandRepository.Update(brand);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Brand.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
