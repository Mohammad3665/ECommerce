using Mapster;
using MapsterMapper;

namespace ECommerce.Application.Common.Mapping;

/// <summary>
/// Registers Mapster object mapping services.
/// </summary>
public static class DependencyInjectionMapping
{
    /// <summary>
    /// Adds Mapster configuration and mapping services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The configured service collection.</returns>
    /// <remarks>
    /// Scans the executing assembly for mapping configurations (IHaveCustomMapping),
    /// registers TypeAdapterConfig as singleton, and IMapper as scoped service.
    /// </remarks>
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
