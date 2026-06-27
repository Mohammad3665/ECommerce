using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryTree;

public class GetCategoryTreeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryTreeQuery, Result<List<CategoryTreeResponseDto>>>
{
    public async Task<Result<List<CategoryTreeResponseDto>>> Handle(GetCategoryTreeQuery request, CancellationToken cancellationToken)
    {
        var allCategories = await unitOfWork.CategoryRepository.GetAllAsync(
            expression: c => c.IsActive,
            cancellationToken: cancellationToken
        );

        var allDtos = allCategories.Select(c => new CategoryTreeResponseDto(
            c.Id,
            c.Name,
            c.Slug,
            c.ParentCategoryId,
            new List<CategoryTreeResponseDto>()
        )).ToList();

        var rootCategories = new List<CategoryTreeResponseDto>();
        var dtoLookup = allDtos.ToDictionary(g => g.Id);

        foreach (var dto in allDtos)
        {
            if (dto.ParentId is null)
            {
                rootCategories.Add(dto);
            }
            else if (dtoLookup.TryGetValue(dto.ParentId.Value, out var parentDto))
            {
                parentDto.Children.Add(dto);
            }
        }

        return Result<List<CategoryTreeResponseDto>>.Success(rootCategories);
    }

}
