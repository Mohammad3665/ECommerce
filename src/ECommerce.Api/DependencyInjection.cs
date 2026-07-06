using ECommerce.Api.Common.Services;
using ECommerce.Api.Services;

namespace ECommerce.Api;

/// <summary>
/// Registers API layer services with dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds API layer services including path and file services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddScoped<IPathService, PathService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
