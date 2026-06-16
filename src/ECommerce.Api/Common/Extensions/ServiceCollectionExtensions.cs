using Asp.Versioning;
using ECommerce.Application;
using ECommerce.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ECommerce.Api.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        services.AddControllers();
        services.AddProblemDetails();
        services.AddOpenApi();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminAndAbove", policy =>
                policy.RequireRole("SuperAdmin", "Admin"));
        });

        return services;
    }
}
