using ECommerce.Api.Contracts.Common;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Common.Extensions;

/// <summary>
/// Provides extension methods to convert <see cref="Result"/> and <see cref="Result{TData}"/> objects
/// into ASP.NET Core <see cref="IActionResult"/> responses.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts a non‑generic <see cref="Result"/> to an <see cref="IActionResult"/>.
    /// Success results become an HTTP 200 OK with a successful API response body;
    /// failure results become a problem details response with the appropriate HTTP status code.
    /// </summary>
    /// <param name="result">The <see cref="Result"/> to convert.</param>
    /// <returns>An <see cref="OkObjectResult"/> on success, or an <see cref="ObjectResult"/> containing
    /// <see cref="ApiProblemDetails"/> on failure.</returns>
    public static IActionResult ToActionResult(this Result result)
        => result.IsSuccess ? new OkObjectResult(ApiResponse.Success()) : CreateProblemDetails(result.Error);

    /// <summary>
    /// Extends <see cref="Result{T}"/> with conversion methods to produce <see cref="IActionResult"/> responses.
    /// </summary>
    /// <typeparam name="T">The type of the data contained in a successful <see cref="Result{T}"/>.</typeparam>
    /// <param name="result">The <see cref="Result{T}"/> instance to convert.</param>
    extension<T>(Result<T> result)
    {
        /// <summary>
        /// Converts the current <see cref="Result{T}"/> to an <see cref="IActionResult"/>.
        /// Success results return HTTP 200 OK with the data wrapped in an <see cref="ApiResponse{T}"/>;
        /// failures return a problem details response with the mapped HTTP status code.
        /// </summary>
        /// <returns>An <see cref="OkObjectResult"/> containing <see cref="ApiResponse{T}"/> on success,
        /// or an <see cref="ObjectResult"/> with problem details on failure.</returns>
        public IActionResult ToActionResult()
            => result.IsSuccess ? new OkObjectResult(ApiResponse<T>.Success(result.Data)) : CreateProblemDetails(result.Error);
    
        /// <summary>
        /// Converts the current <see cref="Result{T}"/> to an <see cref="IActionResult"/>,
        /// mapping the success data to a different type using the provided <paramref name="mapper"/>.
        /// On failure, a problem details response is returned directly without mapping.
        /// </summary>
        /// <typeparam name="TResponse">The type to map the success data to.</typeparam>
        /// <param name="mapper">The mapper used to convert the success data from <typeparamref name="T"/> to <typeparamref name="TResponse"/>.</param>
        /// <returns>An <see cref="OkObjectResult"/> containing <see cref="ApiResponse{TResponse}"/> on success,
        /// or an <see cref="ObjectResult"/> with problem details on failure.</returns>
        public IActionResult ToActionResult<TResponse>(IMapper mapper)
        {
            if (!result.IsSuccess) return CreateProblemDetails(result.Error);

            var responseData = mapper.Map<TResponse>(result.Data);
            return new OkObjectResult(ApiResponse<TResponse>.Success(responseData));
        }
    }
    /// <summary>
    /// Builds an <see cref="ObjectResult"/> with <see cref="ApiProblemDetails"/> from the given error.
    /// The HTTP status code is derived from the error’s type via <see cref="ErrorExtensions.GetStatusCode"/>.
    /// </summary>
    /// <param name="error">The error to represent as problem details.</param>
    /// <returns>An <see cref="ObjectResult"/> with the mapped status code and a problem details body.</returns>
    private static ObjectResult CreateProblemDetails(Error error)
    {
        var statusCode = error.GetStatusCode();
        return new OkObjectResult(new ApiProblemDetails
        {
            Title = error.Code,
            Detail = error.Description,
            Status = statusCode
        });
    }
}