using Serilog;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
var app = builder.Build();
app.UseSerilogRequestLogging();
app.MapControllers();


app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/test", () => Results.Json(new { message = "api is working"}));
app.MapGet("/", () => Results.Redirect("/scalar"));
app.UseHsts();
app.UseHttpsRedirection();


app.Run();