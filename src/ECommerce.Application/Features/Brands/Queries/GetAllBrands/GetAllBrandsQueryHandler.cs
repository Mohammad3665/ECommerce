using ECommerce.Application.Dtos.Brands;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

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

        return Result<IEnumerable<GetBrandResponseDto>>.Success(brands);
    }
}