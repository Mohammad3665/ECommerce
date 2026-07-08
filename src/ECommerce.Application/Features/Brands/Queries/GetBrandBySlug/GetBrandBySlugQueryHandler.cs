using ECommerce.Application.Dtos.Brands;

namespace ECommerce.Application.Features.Brands.Queries.GetBrandBySlug;

public class GetBrandBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBrandBySlugQuery, Result<GetBrandResponseDto>>
{
    public async Task<Result<GetBrandResponseDto>> Handle(GetBrandBySlugQuery request, CancellationToken cancellationToken)
    {
        var brand = await unitOfWork.BrandRepository.GetAsync<GetBrandResponseDto>(
            expression: b => b.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );

        return brand is null ?
            new Error("Brand.NotFound", "برند مورد نظر یافت نشد.", ErrorType.NotFound) :
            brand;
    }
}
