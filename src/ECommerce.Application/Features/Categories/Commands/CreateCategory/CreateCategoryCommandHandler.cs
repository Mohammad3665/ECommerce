using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MapsterMapper;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = request.Adapt<Category>();

        await unitOfWork.CategoryRepository.AddAsync(category, cancellationToken);

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Operation failed.",
                "خطای ناخواسته‌ای هنگام ساخت دسته‌بندی پیش آمد.",
                ErrorType.Unexpected
            );
            return Result<long>.Failure(error);
        }
        return Result<long>.Success(category.Id);
    }
}
