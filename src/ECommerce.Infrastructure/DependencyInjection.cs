using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Interfaces;
using ECommerce.Domain.Common.Stores;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using ECommerce.Infrastructure.Authentication;
using ECommerce.Infrastructure.Common.Services;
using ECommerce.Infrastructure.Identity.Handlers;
using ECommerce.Infrastructure.Identity.Providers;
using ECommerce.Infrastructure.Repositories.Common.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.
                GetConnectionString(StaticDataStore.DefaultSqlServerConnectionStringName)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUnitOfWorkTransactionHandler, UnitOfWorkTransactionHandler>();
        services.AddScoped<IPasswordService, BCryptPasswordService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICurrentUserService, CurrenUserService>();
        services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        return services;
    }
}
