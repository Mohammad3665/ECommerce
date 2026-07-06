using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Specifications.Common;

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
