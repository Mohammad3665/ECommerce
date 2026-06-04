# ECommerce - E-Commerce Platform

[![.NET Version](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-purple.svg)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## 📋 Overview

**ECommerce** is a modern, multi-role e-commerce platform built with **ASP.NET Core 10 Web API** following **Clean Architecture** principles. It provides a complete online shopping experience with product management, shopping cart, orders, discounts, articles, comments, and an advanced role-based access control system.

## 🎯 Features

### Core Features

- ✅ **Multi-Role System** - Owner, Admin, Content Manager, Customer
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

### Technical Features

- 🏗️ **Clean Architecture** - Separation of concerns
- 📦 **CQRS + MediatR** - Command Query Responsibility Segregation
- 🗄️ **Repository + Unit of Work** - Data access patterns
- 🎯 **Result Pattern** - Clean error handling without exceptions
- ✅ **FluentValidation** - Input validation
- 📝 **Serilog + Seq** - Structured logging
- 🔍 **OpenTelemetry** - Distributed tracing
- 🐳 **Docker + Docker Compose** - Containerization
- 💚 **Health Checks** - Service monitoring
- 📚 **Swagger** - API documentation

## 🚀 Technology Stack

| Layer              | Technologies                      |
| ------------------ | --------------------------------- |
| **Framework**      | ASP.NET Core 10 Web API           |
| **Architecture**   | Clean Architecture, CQRS, MediatR |
| **ORM**            | Entity Framework Core 10          |
| **Database**       | SQL Server                        |
| **Authentication** | JWT Bearer                        |
| **Validation**     | FluentValidation                  |
| **Logging**        | Serilog, Seq                      |
| **Observability**  | OpenTelemetry                     |
| **Container**      | Docker, Docker Compose            |
| **API Docs**       | Swagger / OpenAPI                 |