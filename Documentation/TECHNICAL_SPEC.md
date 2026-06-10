# Technical Specification - ECommerce Project

## Project Structure (Clean Architecture)

```markdown
ECommerce/
├── src/
│ ├── ECommerce.Domain/ # Entities, Value Objects, Enums
│ │ ├── Entities/
│ │ ├── Enums/
│ │ └── Interfaces/
│ │
│ ├── ECommerce.Application/ # DTOs, Commands, Queries, Handlers
│ │ ├── Common/
│ │ │ ├── Behaviors/ # Validation, Logging, Caching
│ │ │ ├── Exceptions/
│ │ │ └── Mappings/
│ │ ├── Features/
│ │ │ ├── Products/
│ │ │ │ ├── Commands/
│ │ │ │ ├── Queries/
│ │ │ │ └── Validators/
│ │ │ ├── Orders/
│ │ │ ├── Users/
│ │ │ └── ...
│ │ └── Contracts/ # Request/Response models
│ │
│ ├── ECommerce.Infrastructure/ # Data Access, Caching, External Services
│ │ ├── Persistence/
│ │ │ ├── Context/
│ │ │ ├── Repositories/
│ │ │ ├── Configurations/
│ │ │ └── Migrations/
│ │ ├── Services/
│ │ │ ├── CacheService.cs
│ │ │ ├── PaymentGatewaySimulator.cs
│ │ │ └── ShippingService.cs
│ │ └── Identity/
│ │ ├── JwtService.cs
│ │ └── PasswordHasher.cs
│ │
│ └── ECommerce.API/ # Controllers, Middlewares, Program.cs
│ ├── Controllers/
│ ├── Middlewares/
│ ├── Filters/
│ └── appsettings.json
│
├── tests/ # (Optional - not required for now)
├── docker-compose.yml
├── Dockerfile
└── README.md
```

## OpenTelemetry & Observability Stack

### Architecture Overview

┌─────────────────────────────────────────────────────────────────┐
│ ECommerce Application (.NET 10) │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │ OpenTelemetry SDK │ │
│ │ - Metrics (System.Diagnostics.Metrics) │ │
│ │ - Traces (ActivitySource) │ │
│ │ - Logs (Serilog + OpenTelemetry) │ │
│ └─────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
│
│ OTLP Protocol (gRPC/HTTP)
▼
┌─────────────────────────────────────────────────────────────────┐
│ OpenTelemetry Collector │
│ - Receive: OTLP │
│ - Process: Batch, Filter, Enrich │
│ - Export: Prometheus (Metrics), Jaeger (Traces), Loki (Logs) │
└─────────────────────────────────────────────────────────────────┘
│
┌───────────────────────┼───────────────────────────┐
│ │ │
▼ ▼ ▼
┌───────────────────┐ ┌───────────────────┐ ┌───────────────────┐
│ Prometheus │ │ Jaeger │ │ Loki │
│ (Metrics Store) │ │ (Traces Store) │ │ (Logs Store) │
└───────────────────┘ └───────────────────┘ └───────────────────┘
│ │ │
└───────────────────────┼───────────────────────────┘
│
▼
┌───────────────────────────────────┐
│ Grafana │
│ - Dashboards (Metrics, Traces) │
│ - Exemplars (Metrics → Traces) │
│ - Alerting │
└───────────────────────────────────┘

text

### Components Overview

| Component                   | Role                           | Data Type | Port (Default)           |
| --------------------------- | ------------------------------ | --------- | ------------------------ |
| **OpenTelemetry Collector** | Data collection and processing | All       | 4317 (gRPC), 4318 (HTTP) |
| **Prometheus**              | Metrics storage                | Metrics   | 9090                     |
| **Jaeger**                  | Traces storage                 | Traces    | 16686 (UI), 4317 (OTLP)  |
| **Loki**                    | Logs storage                   | Logs      | 3100                     |
| **Grafana**                 | Visualization and dashboards   | All       | 3000                     |

### OpenTelemetry Configuration in .NET

```csharp
// Program.cs - OpenTelemetry Setup
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);

// ========== Metrics ==========
builder.Services.AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()      // HTTP requests metrics
        .AddHttpClientInstrumentation()      // HttpClient metrics
        .AddRuntimeInstrumentation()         // .NET runtime metrics (GC, memory)
        .AddProcessInstrumentation()         // Process metrics (CPU, memory)
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("System.Net.Http")
        .AddPrometheusExporter());           // Export to Prometheus

// ========== Traces ==========
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation(options =>
        {
            options.RecordException = true;
            options.Filter = (httpContext) =>
                !httpContext.Request.Path.StartsWithSegments("/health");
        })
        .AddHttpClientInstrumentation()
        .AddSqlClientInstrumentation()       // SQL queries tracing
        .AddSource("ECommerce.*")
        .AddJaegerExporter());               // Export to Jaeger

// ========== Logs ==========
builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeFormattedMessage = true;
    options.IncludeScopes = true;
    options.AddConsoleExporter();
    options.AddOtlpExporter();               // Export to Collector
});

var app = builder.Build();

// ========== Expose Metrics Endpoint ==========
app.UseOpenTelemetryPrometheusScrapingEndpoint();  // /metrics
```

## Object Mapping with Mapster

### Why Mapster?

Mapster is selected as the primary object mapper for the ECommerce project due to its superior performance, low memory allocation, and excellent compatibility with Clean Architecture and CQRS patterns.

### Comparison Summary

| Feature                     | Mapster                       | AutoMapper   |
| --------------------------- | ----------------------------- | ------------ |
| **Performance**             | 2-3x faster                   | Baseline     |
| **Memory Allocation**       | Lower                         | Higher       |
| **AOT / NativeAOT Support** | ✅ Yes (via Source Generator) | ❌ Limited   |
| **Code Required**           | ~50% less                     | More verbose |
| **Compile-time Validation** | ✅ (via Source Generator)     | ❌ Runtime   |
| **Learning Curve**          | Gentle                        | Moderate     |

### Installation

```bash
# Core packages
dotnet add package Mapster
dotnet add package Mapster.DependencyInjection

# For Source Generator (optional, for AOT/compile-time validation)
dotnet add package Mapster.Core
```

### Configuration

```csharp
// Program.cs
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Configure Mapster (scan assemblies for mappings)
TypeAdapterConfig.GlobalSettings.Scan(typeof(Program).Assembly);

// Register Mapster
builder.Services.AddMapster();

var app = builder.Build();
// ... rest of the app
```

# appsettings.json

```json
{
  "Redis": {
    "ConnectionString": "redis:6379,abortConnect=false",
    "InstanceName": "ECommerce"
  }
}
```
