using Serilog;
using ECommerce.Api;
using ECommerce.Api.Common.Extensions;
try
{
    var app = StartupHost.Build(args);
    app.UseApplicationMiddlewares();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unexpected error occurred. Application is shutting down.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}