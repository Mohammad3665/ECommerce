using ECommerce.Application.Common.Behaviors;
using ECommerce.Application.Common.Configurations;

namespace ECommerce.Application;

/// <summary>
/// Registers application layer services with dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application layer services including mapping, MediatR, and validation.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The configured service collection.</returns>
    /// <remarks>
    /// Registers Mapster for object mapping, MediatR with logging pipeline behavior,
    /// and configures FluentValidation for request validation.
    /// </remarks>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add mapping(mapster)
        services.AddMapping();

        // Add mediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        });

        // Add pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        // Configure Fluent Validation
        FluentValidationConfig.Configure();
        return services;
    }
}
