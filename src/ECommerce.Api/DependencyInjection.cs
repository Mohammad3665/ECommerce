using ECommerce.Api.Common.Services;
using ECommerce.Api.Services;
using ECommerce.Application.Common.Interfaces.Services;

namespace ECommerce.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddScoped<IPathService, PathService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
