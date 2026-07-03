using ECommerce.Application.Dtos.Brands;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Brands.Queries.GetBrandBySlug;

public class GetBrandBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetBrandBySlugQuery, Result<GetBrandResponseDto>>
{
    public async Task<Result<GetBrandResponseDto>> Handle(GetBrandBySlugQuery request, CancellationToken cancellationToken)
    {
        var brandDto = await unitOfWork.BrandRepository.GetAsync<GetBrandResponseDto>(
            expression: b => b.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (brandDto is null)
        {
            var error = new Error(
                "Brand.NotFound",
                "برند مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result<GetBrandResponseDto>.Failure(error);
        }

        return Result<GetBrandResponseDto>.Success(brandDto);
    }
}
