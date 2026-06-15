using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IUnitOfWorkTransactionHandler handler) : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        => await handler.ExecuteAsync(async (uow, token) =>
        {
            var category = await uow.CategoryRepository.GetAsync(
                expression: c => c.Id == request.Id,
                cancellationToken: token
            );

            if (category is null)
            {
                var error = new Error(
                    "Category.NotFound",
                    $"Category with Id {request.Id} was not found.",
                    ErrorType.NotFound
                );
                return Result.Failure(error);
            }

            var product = await uow.ProductRepository.GetAsync(
                expression: p => p.CategoryId == request.Id, 
                cancellationToken: token
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

            uow.CategoryRepository.DeletePermanently(category);
            var saveResult = await uow.SaveAsync(token);
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
        }, cancellationToken);
}