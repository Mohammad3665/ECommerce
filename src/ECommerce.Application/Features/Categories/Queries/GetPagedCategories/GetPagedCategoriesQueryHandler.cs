using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public class GetPagedCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedCategoriesQuery, Result<Pagination<GetPagedCategoriesResponseDto>>>
{
    public async Task<Result<Pagination<GetPagedCategoriesResponseDto>>> Handle(GetPagedCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.CategoryRepository.GetPagedListAsync<GetPagedCategoriesResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );
        if (categories is null)
            return new Error("Category.NotFound", "هیچ دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return categories;
    }
}
