using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryDto = await unitOfWork.CategoryRepository.GetAsync(
            expression: c => c.Id == request.Id,
            selector: src => src.Adapt<CategoryDto>(),
            cancellationToken: cancellationToken
        );

        if (categoryDto is null)
        {
            var error = new Error(
                "Category.NotFound", 
                "Category not found.", 
                ErrorType.NotFound
            );
            return Result<CategoryDto>.Failure(error);
        }
        return Result<CategoryDto>.Success(categoryDto);
    }
}