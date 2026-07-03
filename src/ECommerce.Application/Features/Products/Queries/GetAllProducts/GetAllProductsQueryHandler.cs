using System.Collections;
using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

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

        return Result<IEnumerable<GetAllProductsResponseDto>>.Success(products);
    }
}