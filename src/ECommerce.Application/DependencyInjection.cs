using System.Reflection;
using ECommerce.Application.Common.Behaviors;
using ECommerce.Application.Common.Configurations;
using ECommerce.Application.Common.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class DependencyInjection
{
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
        // configure Fluent Validation
        FluentValidationConfig.Configure();
        return services;
    }
}
