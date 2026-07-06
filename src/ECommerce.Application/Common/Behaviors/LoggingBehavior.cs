using System.Diagnostics;
using ECommerce.Domain.Common.Result.Base;

namespace ECommerce.Application.Common.Behaviors;

/// <summary>
/// Logs request execution time and results for MediatR pipeline.
/// </summary>
/// <typeparam name="TRequest">The request type (e.g., CreateProductCommand).</typeparam>
/// <typeparam name="TResponse">The response type inherited from BaseResult.</typeparam>
/// <remarks>
/// Logs start/completion with performance metrics, distinguishes business failures from exceptions.
/// </remarks>
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResult
{
    /// <summary>
    /// Handles request with performance logging and error tracking.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">Next pipeline handler.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response wrapped in BaseResult.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("🚀 [START] Handling {RequestName}", requestName);

        var stopWatch = Stopwatch.StartNew();
        TResponse response;

        try
        {
            response = await next();
        }
        catch(Exception ex)
        {
            stopWatch.Stop();
            logger.LogError(ex, 
                "💥 [FAIL] {RequestName} failed after {ElapsedMilliseconds}ms with unhandled exception", 
                requestName, 
                stopWatch.ElapsedMilliseconds);
            throw;
        }

        stopWatch.Stop();

        if (!response.IsSuccess)
        {
            logger.LogWarning("⚠️ [WARN] {RequestName} processed with failure error: {@Error} (Took {ElapsedMilliseconds}ms)", 
                requestName, 
                response.Error, 
                stopWatch.ElapsedMilliseconds);
        }
        else
        {
            logger.LogInformation("✅ [END] {RequestName} successfully completed in {ElapsedMilliseconds}ms", 
                requestName, stopWatch.ElapsedMilliseconds);
        }
        return response;
    }
}