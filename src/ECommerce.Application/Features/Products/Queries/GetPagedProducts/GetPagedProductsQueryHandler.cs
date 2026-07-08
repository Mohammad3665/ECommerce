using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Products.Queries.GetPagedProducts;

public class GetPagedProductsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedProductsQuery, Result<Pagination<GetPagedProductsResponseDto>>>
{
    public async Task<Result<Pagination<GetPagedProductsResponseDto>>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = await unitOfWork.ProductRepository.GetPagedListAsync<GetPagedProductsResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );
        return pagedResult;
    }
}
