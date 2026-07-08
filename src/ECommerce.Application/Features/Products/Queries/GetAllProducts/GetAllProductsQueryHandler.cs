using ECommerce.Application.Dtos.Products;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<GetAllProductsResponseDto>>>
{
    public async Task<Result<IEnumerable<GetAllProductsResponseDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.ProductRepository.GetAllAsync<GetAllProductsResponseDto>(
            expression: null,
            order: query => query.OrderBy(p => p.CreatedAt),
            cancellationToken: cancellationToken,
            includes: [
                p => p.Category,
                p => p.Brand
            ]
        );
        if (products is null || !products.Any())
            return new Error("Product.NotFound", "هیچ دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<GetAllProductsResponseDto>>.Success(products);
    }
}
