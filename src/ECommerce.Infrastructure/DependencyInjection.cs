using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Interfaces;
using ECommerce.Domain.Common.Stores;
using ECommerce.Infrastructure.Authentication;
using ECommerce.Infrastructure.Common.Services;
using ECommerce.Infrastructure.Identity.Handlers;
using ECommerce.Infrastructure.Identity.Providers;
using ECommerce.Infrastructure.Persistence.Interceptors;
using ECommerce.Infrastructure.Repositories.Common.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;

namespace ECommerce.Infrastructure;

/// <summary>
/// Registers infrastructure layer services with dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services including database, repositories, and core services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The configured service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Domain Events
        services.AddScoped<DispatchDomainEventsInterceptor>();

        // Database
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString(StaticDataStore.DefaultSqlServerConnectionStringName));
            options.AddInterceptors(sp.GetRequiredService<DispatchDomainEventsInterceptor>());
        });

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWorkTransactionHandler, UnitOfWorkTransactionHandler>();

        // Core Services
        services.AddScoped<IPasswordService, BCryptPasswordService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();

        // Product Services
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<IProductSpecificationService, ProductSpecificationService>();

        // Slug Service
        services.AddScoped<ISlugService, SlugService>();

        // User Context
        services.AddScoped<ICurrentUserService, CurrenUserService>();

        // Authorization
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

        // Cart Service
        services.AddScoped<ICartService, RedisCartService>();
        services.AddSingleton<IConnectionMultiplexer>(provider =>
            ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]!));

        // Redis Caching
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration["Redis:ConnectionString"]!;
            return ConnectionMultiplexer.Connect(connectionString);
        });

        // Payment Service
        services.AddHttpClient<IPaymentService, ZarinPalPaymentService>();

        // Html Sanitizer Service
        services.AddScoped<IHtmlSanitizerService, HtmlSanitizerService>();

        return services;
    }
}