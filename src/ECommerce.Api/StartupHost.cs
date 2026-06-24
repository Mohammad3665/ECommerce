using System.IdentityModel.Tokens.Jwt;
using ECommerce.Api.Common.Extensions;
using Mapster;
using Serilog;

namespace ECommerce.Api;

public class StartupHost
{
    public static WebApplication Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        builder.Host.UseSerilog();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddHttpContextAccessor();
        return builder.Build();
    }
}
