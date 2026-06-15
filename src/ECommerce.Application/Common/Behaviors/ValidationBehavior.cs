using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Common.Result.Base;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ECommerce.Application.Common.Behaviors;

/// <summary>
/// A MediatR pipeline behavior that executes all <see cref="IValidator{TRequest}"/> instances
/// associated with the request before it reaches the handler.  
/// If validation fails, it returns a <see cref="BaseResult"/> with the validation errors
/// instead of calling the handler.
/// </summary>
/// <typeparam name="TRequest">The type of the request being validated.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the handler. Must inherit <see cref="BaseResult"/>.</typeparam>
public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResult
{
    /// <summary>
    /// Handles the request by executing all validators.  
    /// If there are any validation failures, a failure result is returned immediately.
    /// Otherwise, the request is forwarded to the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">The delegate representing the next handler in the pipeline.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>
    /// A task that returns either a validation failure <see cref="BaseResult"/> or the result from the next handler.
    /// </returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if(!validators.Any())
            return await next();
        
        var context = new ValidationContext<TRequest>(request);
        var validationResult = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResult
            .SelectMany(e => e.Errors)
            .Where(f => f != null)
            .ToList();
        
        if (failures.Count() is not 0)
            return CreateValidationErrorResult<TResponse>(failures);

        return await next(cancellationToken);
    }

    /// <summary>
    /// Creates a validation failure result of the specified type <typeparamref name="TRes"/>.  
    /// Supports <see cref="Result"/>, <see cref="Result{T}"/>, and any generic <see cref="Result{T}"/> type.
    /// </summary>
    /// <typeparam name="TRes">The type of the result to create.</typeparam>
    /// <param name="failures">The list of <see cref="ValidationFailure"/>.</param>
    /// <returns>A <typeparamref name="TRes"/> containing the validation error messages.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the type is not supported.</exception>
    private static TRes CreateValidationErrorResult<TRes>(List<ValidationFailure> failures)
    {
        var combinedMessage = string.Join(" | ", failures.Select(f => f.ErrorMessage));
        var error = new Error("ValidationError", combinedMessage, ErrorType.Validation);
        if (typeof(TRes) == typeof(Result<long>))
        {
            var result = Result<long>.Failure(error);
            return (TRes)(object)result;
        }
        if (typeof(TRes) == typeof(Result))
        {
            var result = Result.Failure(error);
            return (TRes)(object)result;
        }

        var resultType = typeof(TRes);
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var dataType = resultType.GetGenericArguments()[0];
            var failureMethod = typeof(Result<>)
                .MakeGenericType(dataType)
                .GetMethod("Failure", [typeof(Error)]);
            
            if (failureMethod is not null)
            {
                var result = failureMethod.Invoke(null, [error]);
                return (TRes)result!;
            }
        }
        throw new InvalidOperationException($"Unsupported result type: {typeof(TRes)}");
    }
}