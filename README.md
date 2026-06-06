# ECommerce - E-Commerce Platform

[![.NET Version](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-purple.svg)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## 📋 Overview

**ECommerce** is a modern, multi-role e-commerce platform built with **ASP.NET Core 10 Web API** following **Clean Architecture** principles. It provides a complete online shopping experience with product management, shopping cart, orders, discounts, articles, comments, and an advanced role-based access control system.

## 🎯 Features

### Core Features

- ✅ **Multi-Role System** - Super Admin, Admin, Content Manager, Customer
- ✅ **JWT Authentication** - Secure token-based authentication
- ✅ **Product Management** - CRUD operations with images and specifications
- ✅ **Brand & Category Management** - Organize products efficiently
- ✅ **Advanced Search & Filtering** - Filter by price, brand, category, stock
- ✅ **Shopping Cart** - Add/remove items, update quantities
- ✅ **Order Management** - Place orders, track status, view history
- ✅ **Coupon System** - Percentage and fixed amount discounts
- ✅ **Comment System** - Nested comments with admin approval
- ✅ **Article System** - Blog-style content management
- ✅ **Slider Management** - Dynamic homepage sliders
- ✅ **Admin Dashboard** - Sales reports, statistics, order status
- ✅ **SEO Friendly URLs** - Slug-based routing for better SEO

### Reporting & Invoice Features (New)

- ✅ **Invoice Generation** - Auto-generated PDF invoices after successful payment
- ✅ **Unique Invoice Numbers** - Sequential format: INV-YYYYMMDD-XXXX
- ✅ **Invoice Download** - Customers can download invoices from order history
- ✅ **Email Invoices** - Send invoices directly to customer email
- ✅ **Daily Sales Report** - View sales data grouped by day
- ✅ **Date Range Filtering** - Filter reports by custom date range
- ✅ **Export to Excel** - Download reports as Excel files
- ✅ **Export to PDF** - Download reports as PDF files
- ✅ **Sales Metrics** - Total orders, total sales, average order value, tax collected, shipping cost

### Technical Features

- 🏗️ **Clean Architecture** - Separation of concerns
- 📦 **CQRS + MediatR** - Command Query Responsibility Segregation
- 🗄️ **Repository + Unit of Work** - Data access patterns
- 🎯 **Result Pattern** - Clean error handling without exceptions
- ✅ **FluentValidation** - Input validation
- 📝 **Serilog + Seq** - Structured logging
- 🔍 **OpenTelemetry + Prometheus and grafana** - Distributed tracing
- 🐳 **Docker + Docker Compose** - Containerization
- 💚 **Health Checks** - Service monitoring
- 📚 **Scalar** - API documentation
- 📊 **EPPlus** - Excel report generation
- 📄 **QuestPDF** - PDF invoice generation

## 🚀 Technology Stack

| Layer              | Technologies                           |
| ------------------ | -------------------------------------- |
| **Framework**      | ASP.NET Core 10 Web API                |
| **Architecture**   | Clean Architecture, CQRS, MediatR      |
| **ORM**            | Entity Framework Core 10               |
| **Database**       | SQL Server                             |
| **Authentication** | JWT Bearer                             |
| **Validation**     | FluentValidation                       |
| **Logging**        | Serilog, Seq                           |
| **Observability**  | OpenTelemetry + Prometheus and grafana |
| **Container**      | Docker, Docker Compose                 |
| **API Docs**       | Scalar / OpenAPI                       |
| **Cahing**         | Redis                                  |

## 📚 API Endpoints Overview

| Module         | Base Path                 | Description                                    |
| -------------- | ------------------------- | ---------------------------------------------- |
| Authentication | `/api/v1/Auth`            | Register, login, token refresh                 |
| Users          | `/api/v1/Admin/Users`     | User management (Admin/Super User)             |
| Roles          | `/api/v1/Owner/Roles`     | Role & permission management (Super User only) |
| Profile        | `/api/v1/Profile`         | User profile management                        |
| Products       | `/api/v1/Products`        | Product catalog                                |
| Categories     | `/api/v1/Categories`      | Category & subcategory management              |
| Brands         | `/api/v1/Brands`          | Brand management                               |
| Cart           | `/api/v1/Cart`            | Shopping cart operations                       |
| Orders         | `/api/v1/Orders`          | Order placement & tracking                     |
| Invoices       | `/api/v1/Invoices`        | Invoice generation & download                  |
| Reports        | `/api/v1/Admin/Reports`   | Daily sales reports & exports                  |
| Comments       | `/api/v1/Comments`        | Product comments                               |
| Articles       | `/api/v1/Articles`        | Blog articles                                  |
| Dashboard      | `/api/v1/Admin/Dashboard` | Admin statistics                               |
