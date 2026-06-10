# ECommerce Project - Project Context

## Project Identification

- **Project Name:** ECommerce (E-commerce Training Platform)
- **Version:** 1.0.0
- **Start Date:** 2025
- **Project Type:** E-commerce Web API

## Project Summary

A multi-role e-commerce platform with product management, brand management, category management, articles, shopping cart, orders, discount coupons, and comment system.

## Business Objectives

1. Provide a multi-level access control e-commerce platform
2. Enhance user experience with advanced filters and search
3. Dynamic content management (articles, sliders)
4. Support various discount types (percentage/fixed amount)
5. Use of generic BaseEntity and BaseRepository for Repetitive properties and oprations

## Stakeholders

| Role            | Responsibility                                    |
| --------------- | ------------------------------------------------- |
| Super Admin     | Full system access, user and role management      |
| Admin           | Product, brand, category, and order management    |
| Content Manager | Article management, approve/reject comments       |
| Customer        | Purchase products, submit comments, view products |

## Constraints

- Training project (No CI/CD, no unit testing required)
- Caching with redis
- Simulated payment gateway (No real payment integration)
- Fixed shipping rate (Post service only)

## Assumptions

- Users must be authenticated to place orders and post comments
- Each product belongs to exactly one brand and one category
- Comments require approval by Content Manager before display
- Stock quantity decreases automatically upon order placement
- The system supports **unlimited depth** for category nesting using a self-referencing foreign key pattern.
- Currency: Iranian Toman (Tomans)

## Observability Stack (OpenTelemetry + Prometheus + Grafana)

### Overview

The project uses a complete observability stack for monitoring, tracing, and alerting:

| Component                   | Purpose                                                   |
| --------------------------- | --------------------------------------------------------- |
| **OpenTelemetry SDK**       | Generate and collect telemetry data from .NET application |
| **OpenTelemetry Collector** | Process, batch, and route telemetry data                  |
| **Prometheus**              | Store and query metrics data                              |
| **Jaeger**                  | Store and visualize distributed traces                    |
| **Loki**                    | Store and query structured logs                           |
| **Grafana**                 | Dashboards, visualization, and alerting                   |

### Key Benefits

1. **Vendor-neutral** - Not locked into any commercial vendor
2. **Cost-effective** - Open-source, no licensing costs
3. **Complete observability** - Metrics, traces, and logs in one place
4. **Exemplars support** - Jump from metric spike to exact trace
5. **Custom dashboards** - Tailored for e-commerce KPIs

### Grafana Dashboards

- **Business KPIs**: Daily sales, order count, revenue
- **Technical Metrics**: Request rate, error rate, response time
- **Infrastructure**: CPU, memory, GC, thread pool
- **Custom Dashboards**: Per endpoint, per user, per product

## Shopping Cart Storage

### Technology: Redis

The shopping cart has been migrated from SQL Server to Redis to achieve:

- **Faster response times** for cart operations
- **Reduced database load** (SQL Server handles orders only)
- **Infinite TTL** (cart persists until user checks out)
- **Simpler architecture** (no complex relationships)

### Current Status

| Component              | Status       |
| ---------------------- | ------------ |
| SQL Server Cart Tables | ❌ Removed   |
| Redis Cart Storage     | ✅ Active    |
| Cart API Endpoints     | ✅ Unchanged |

### Data Persistence

Redis is configured with:

- **AOF (Append Only File)** for durability
- **Docker Volume** for persistent storage
- **No automatic expiration** (TTL = infinite)

### Cart Lifecycle

### Alert Rules

- High error rate (>5% for 5 minutes)
- Slow response time (p95 > 1000ms)
- Low stock alert (<5 items)
- Order failure rate spike

## Core Technologies

- ASP.NET Core 10 Web API
- Entity Framework Core 10
- SQL Server
- Clean Architecture
- CQRS + MediatR
- Repository + Unit of Work
- JWT Authentication
- FluentValidation
- Serilog + Seq
- OpenTelemetry
- Docker + Docker Compose
- Health Checks
- Scalar
- Redis
- Mapster
