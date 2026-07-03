using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductBySlug;

public class GetProductBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductBySlugQuery, Result<GetProductResponseDto>>
{
    public async Task<Result<GetProductResponseDto>> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetAsync<GetProductResponseDto>(
            expression: p => p.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (product is null)
        {
            var error = new Error(
                "Product.NotFound",
                "محصول مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result<GetProductResponseDto>.Failure(error);
        }

        return Result<GetProductResponseDto>.Success(product);
    }
}