using ECommerce.Application.Dtos.Brands;

namespace ECommerce.Application.Features.Brands.Queries.GetAllBrands;

public class GetAllBrandsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllBrandsQuery, Result<IEnumerable<GetBrandResponseDto>>>
{
    public async Task<Result<IEnumerable<GetBrandResponseDto>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await unitOfWork.BrandRepository.GetAllAsync<GetBrandResponseDto>(
            expression: null,
            order: query => query.OrderBy(b => b.Name),
            cancellationToken: cancellationToken
        );
        if (brands is null || !brands.Any())
            return new Error("Brand.NotFound", "هیچ دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<GetBrandResponseDto>>.Success(brands);
    }
}
