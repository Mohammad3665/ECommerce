using System.Diagnostics;
using ECommerce.Domain.Common.Result.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerce.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResult
{
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