using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public record GetPagedCategoriesQuery(
    int PageNumber, 
    int PageSize, 
    string? SearchTerm
) : IRequest<Result<Pagination<PagedCategoryDto>>>;
