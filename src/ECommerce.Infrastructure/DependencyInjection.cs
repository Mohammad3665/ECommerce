using ECommerce.Domain.Common.Stores;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using ECommerce.Infrastructure.Repositories.Common.UnitOfWork;
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

        return services;
    }
}
