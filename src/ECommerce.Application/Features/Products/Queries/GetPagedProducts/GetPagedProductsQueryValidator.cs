using ECommerce.Application.Common.Validators;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Features.Products.Queries.GetPagedProducts;

public class GetPagedProductsQueryValidator : QueryRequestValidator<GetPagedProductsQuery, Product>
{
    public GetPagedProductsQueryValidator() { }
}
