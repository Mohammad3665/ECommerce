using ECommerce.Application.Dtos.Products;

namespace ECommerce.Application.Features.Products.Queries.GetLowStockProducts;

public class GetLowStockProductsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLowStockProductsQuery, Result<IEnumerable<LowStockProductDto>>>
{
    public async Task<Result<IEnumerable<LowStockProductDto>>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.ProductRepository.GetAllAsync<LowStockProductDto>(
            expression: p => p.StockQuantity < 5,
            cancellationToken: cancellationToken
        );
        if (products is null || !products.Any())
            return new Error("Product.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<LowStockProductDto>>.Success(products);
    }
}
