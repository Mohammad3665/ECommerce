using Serilog;
using ECommerce.Api;
using ECommerce.Api.Common.Extensions;
try
{
    var app = StartupHost.Build(args);
    await app.UseApplicationMiddlewares();
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unexpected error occurred. Application is shutting down.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}