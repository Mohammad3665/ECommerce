using System.Linq.Expressions;
using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using ECommerce.Domain.Specifications.Common;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public class GetPagedCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedCategoriesQuery, Result<Pagination<GetPagedCategoriesResponseDto>>>
{
    public async Task<Result<Pagination<GetPagedCategoriesResponseDto>>> Handle(GetPagedCategoriesQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = await unitOfWork.CategoryRepository.GetPagedListAsync<GetPagedCategoriesResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );
        return Result<Pagination<GetPagedCategoriesResponseDto>>.Success(pagedResult);
    }
}
