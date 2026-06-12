using Serilog;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Common.Stores;
using ECommerce.Infrastructure.DatabaseContext;
using ECommerce.Infrastructure.Common.Extensions;
using Mapster;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

var connectionString = builder.Configuration.GetConnectionString(StaticDataStore.DefaultSqlServerConnectionStringName);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMapster();

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
var app = builder.Build();
app.UseSerilogRequestLogging();
app.MapControllers();

app.Services.ApplyMigrations();

app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/test", () => Results.Json(new { message = "api is working"}));
app.MapGet("/", () => Results.Redirect("/scalar"));
app.UseHsts();
app.UseHttpsRedirection();


app.Run();