using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Interfaces;
using ECommerce.Domain.Common.Stores;
using ECommerce.Infrastructure.Authentication;
using ECommerce.Infrastructure.Common.Services;
using ECommerce.Infrastructure.Identity.Handlers;
using ECommerce.Infrastructure.Identity.Providers;
using ECommerce.Infrastructure.Repositories.Common.UnitOfWork;
using Microsoft.AspNetCore.Authorization;

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
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.
                GetConnectionString(StaticDataStore.DefaultSqlServerConnectionStringName)));

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWorkTransactionHandler, UnitOfWorkTransactionHandler>();

        // Core Services
        services.AddScoped<IPasswordService, BCryptPasswordService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();

        // User Context
        services.AddScoped<ICurrentUserService, CurrenUserService>();

        // Authorization
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

        return services;
    }
}
