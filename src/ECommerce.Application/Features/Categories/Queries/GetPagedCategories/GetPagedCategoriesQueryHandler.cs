using System.Linq.Expressions;
using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using ECommerce.Domain.Specifications.Common;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public class GetPagedCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedCategoriesQuery, Result<Pagination<PagedCategoryDto>>>
{
    public async Task<Result<Pagination<PagedCategoryDto>>> Handle(GetPagedCategoriesQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Category, bool>>? filterExpression = null;
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim().ToLower();
            filterExpression = c => c.Name.ToLower().Contains(search) ||
                c.EnglishName.ToLower().Contains(search);
        }
        var pagedResult = await unitOfWork.CategoryRepository.GetAllAsync(
            current: request.PageNumber,
            take: request.PageSize,
            selector: src => src.Adapt<PagedCategoryDto>(),
            expression: filterExpression,
            order: o => o.OrderByDescending(c => c.Id),
            cancellationToken: cancellationToken
        );
        return Result<Pagination<PagedCategoryDto>>.Success(pagedResult);
    }
}
