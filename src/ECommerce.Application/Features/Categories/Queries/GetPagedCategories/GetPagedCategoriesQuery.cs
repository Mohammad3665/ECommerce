using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public class GetPagedCategoriesQuery : QueryRequest, IRequest<Result<Pagination<GetPagedCategoriesResponseDto>>>
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
    string? SearchTerm { get; set; } = string.Empty;
}