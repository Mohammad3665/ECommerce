using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Products.Queries.GetPagedProducts;

public class GetPagedProductsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedProductsQuery, Result<Pagination<GetPagedProductsResponseDto>>>
{
    public async Task<Result<Pagination<GetPagedProductsResponseDto>>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.ProductRepository.GetPagedListAsync<GetPagedProductsResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );
        if (products is null)
            return new Error("Product.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return products;
    }
}
