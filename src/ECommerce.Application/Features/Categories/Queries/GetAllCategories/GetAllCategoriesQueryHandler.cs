using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryDto>>>
{
    public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.CategoryRepository.GetAllAsync(
            selector: src => src.Adapt<CategoryDto>(),
            expression: null,
            order: query => query.OrderBy(c => c.Name),
            cancellationToken: cancellationToken
        );

        return Result<IEnumerable<CategoryDto>>.Success(categories);
    }
}