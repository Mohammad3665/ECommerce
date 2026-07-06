using System.IdentityModel.Tokens.Jwt;
using Serilog;

namespace ECommerce.Api;

/// <summary>
/// Bootstraps and configures the web application host.
/// </summary>
/// <remarks>
/// Initializes logging, clears JWT claim mapping, and registers application services.
/// </remarks>
public class StartupHost
{
    /// <summary>
    /// Builds and configures the WebApplication instance.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the application.</param>
    /// <returns>A configured WebApplication ready to run.</returns>
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