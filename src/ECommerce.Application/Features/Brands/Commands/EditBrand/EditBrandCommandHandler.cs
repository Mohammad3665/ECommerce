using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Brands.Commands.EditBrand;

public class EditBrandCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditBrandCommand, Result>
{
    public async Task<Result> Handle(EditBrandCommand request, CancellationToken cancellationToken)
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

        brand.Slug = request.EnglishName.ToSlug();
        var config = new TypeAdapterConfig();
        config.NewConfig<EditBrandCommand, Brand>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Slug);
            
        request.Adapt(brand, config);

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
