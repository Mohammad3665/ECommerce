using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public class GetPagedCategoriesQuery : QueryRequest, IRequest<Result<Pagination<GetPagedCategoriesResponseDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
}