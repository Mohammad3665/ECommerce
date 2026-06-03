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
| Owner           | Full system access, user and role management      |
| Admin           | Product, brand, category, and order management    |
| Content Manager | Article management, approve/reject comments       |
| Customer        | Purchase products, submit comments, view products |

## Constraints

- Training project (No CI/CD, no unit testing required)
- In-memory caching only (No Redis)
- Simulated payment gateway (No real payment integration)
- Fixed shipping rate (Post service only)

## Assumptions

- Users must be authenticated to place orders and post comments
- Each product belongs to exactly one brand and one category
- Comments require approval by Content Manager before display
- Stock quantity decreases automatically upon order placement
- Currency: Iranian Toman (Tomans)

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
- Swagger
