using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(
                expression: c => c.Id == request.Id,
                cancellationToken: cancellationToken
            );

        if (category is null)
        {
            var error = new Error(
                "Category.NotFound",
                $"دسته‌بندی با شناسه {request.Id} یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.CategoryId == request.Id,
            cancellationToken: cancellationToken
        );
        if (product is not null)
        {
            var error = new Error(
                "Category.CannotDeleteWithProducts",
                "This category has products and cannot be deleted. First, move or delete the products.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        unitOfWork.CategoryRepository.DeletePermanently(category);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Operation failed.",
                "An unexpected error occurred while deleting the category.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }
        return Result.Success();
    }
}