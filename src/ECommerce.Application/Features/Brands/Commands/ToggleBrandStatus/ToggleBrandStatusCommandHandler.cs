using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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
        {
            var error = new Error(
                "Brand.NotFound",
                $"برند یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        brand.IsActive = request.IsActive;

        unitOfWork.BrandRepository.Update(brand);
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

        return Result.Success();
    }
}
