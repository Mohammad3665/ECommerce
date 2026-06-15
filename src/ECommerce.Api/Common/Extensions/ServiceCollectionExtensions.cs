using ECommerce.Application;
using ECommerce.Infrastructure;
using Mapster;

namespace ECommerce.Api.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        services.AddOpenApi();
        services.AddControllers();

        return services;
    }
}
