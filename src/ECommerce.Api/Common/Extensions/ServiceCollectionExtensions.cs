using System.Text;
using Asp.Versioning;
using ECommerce.Api.Middlewares;
using ECommerce.Application;
using ECommerce.Domain.Common.Stores;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Api.Common.Extensions;

/// <summary>
/// Registers all application services with dependency injection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all required services for the application.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The configured service collection.</returns>
    /// <remarks>
    /// Registers infrastructure, application, API services, controllers, exception handlers,
    /// OpenAPI documentation, API versioning, JWT authentication, and custom authorization policies.
    /// </remarks>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddApplication();
        services.AddApi();
        services.AddControllers();

        var connectionString = configuration.GetConnectionString(StaticDataStore.DefaultSqlServerConnectionStringName);
        var redisConnectionString = configuration["Redis:ConnectionString"];

        services.AddHealthChecks()
            .AddSqlServer(connectionString: connectionString!, name: "database", tags: ["db", "ready"])
            .AddRedis(redisConnectionString: redisConnectionString!, name: "redis", tags: ["cache", "ready"])
            .AddUrlGroup(
                new Uri("http://seq:80"),
                name: "seq",
                tags: ["logging", "ready"])
            .AddUrlGroup(
                new Uri("http://mailhog:8025"),
                name: "mailhog",
                tags: ["email", "ready"]);
        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();
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
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!)),
                NameClaimType = "sub",
                RoleClaimType = "role",
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPanelAccess", policy =>
            policy.RequireAssertion(context =>
            {
                var userRole = context.User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value
                            ?? context.User.FindFirst("role")?.Value;

                return !string.IsNullOrEmpty(userRole) && userRole != "Customer";
            }));
        });

        return services;
    }
}
