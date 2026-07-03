# ECommerce System Requirements

## Functional Requirements

### 1. User Management & Authentication

- [x] FR-001: User registration with default Customer role
- [x] FR-002: Login with JWT token
- [x] FR-003: User creation by Admin/Super Admin with custom role assignment
- [x] FR-004: User profile editing
- [x] FR-005: Password change functionality
- [x] FR-006: User list display for Admin/Super Admin
- [x] FR-007: Enable/disable user accounts
- [x] FR-008: Default role management (Super Admin, Admin, ContentManager, Customer)

### 2. Brand Management

- [x] FR-009: Create new brand
- [x] FR-010: Edit brand information
- [x] FR-011: Delete brand (with product dependency check)
- [x] FR-012: Activate/deactivate brand
- [x] FR-013: Assign brand to products
- [x] FR-014: Change product's brand
- [x] FR-015: View products by brand

### 3. Category Management

- [x] FR-016: Complete CRUD operations for categories
- [x] FR-017: Activate/deactivate category
- [x] FR-018: View products by category

### 4. Product Management

- [x] FR-019: Complete CRUD operations for products
- [x] FR-020: Multiple image upload for products
- [x] FR-021: Product specifications and attributes
- [x] FR-022: Inventory management
- [x] FR-023: Low stock alert (below 5 units)
<!-- - [ ] FR-024: Activate/deactivate product -->

### 5. Product Search & Filtering

- [x] FR-025: Keyword search
- [x] FR-026: Filter by brand and category
- [x] FR-027: Price range filter (from-to)
- [x] FR-028: Stock filter (in-stock/out-of-stock)
- [x] FR-029: Sorting (most expensive, cheapest, most viewed, newest)
- [x] FR-030: Pagination

### 6. Shopping Cart & Orders

- [ ] FR-031: Add/remove products from cart
- [ ] FR-032: Edit product quantity in cart
- [ ] FR-033: Display cart total amount
- [ ] FR-034: Place order in 3 steps (info, shipping, payment)
- [ ] FR-035: Automatic stock decrease after order placement
- [ ] FR-036: Order history in user profile

### 7. Discount System (Coupons)

- [ ] FR-037: Create percentage discount coupons (e.g., 20%)
- [ ] FR-038: Create fixed amount discount coupons (e.g., 50,000 Tomans)
- [ ] FR-039: Coupon expiration date
- [ ] FR-040: Apply coupon to shopping cart

### 8. Articles & Content

- [ ] FR-041: Article CRUD by Super Admin / Admin / ContentManager
- [ ] FR-042: Article categorization
- [ ] FR-043: Display latest articles on homepage
- [ ] FR-044: View article details

### 9. Comment System

- [ ] FR-045: Comment submission by authenticated users only
- [ ] FR-046: Nested replies to comments
- [ ] FR-047: Comment approval by ContentManager/Admin/Super Admin
- [ ] FR-048: Display approved comments on product page

### 10. Slider Management

- [ ] FR-049: Slider CRUD by Admin/Super Admin
- [ ] FR-050: Each slide includes (image, title, description, link)
- [ ] FR-051: Slide display order configuration

### 11. Admin Dashboard

- [ ] FR-052: Sales report (total amount, order count)
- [ ] FR-053: Most viewed products display
- [ ] FR-054: Order status report (Pending, Paid, Shipping, Delivered, Cancelled)

### 12. Payment Module (Simulated)

- [ ] FR-055: Sandbox payment gateway simulation
- [ ] FR-056: Successful/failed payment confirmation
- [ ] FR-057: Tracking code generation after payment

## Functional Requirements

### 13. Slug & SEO Friendly URLs

- [x] FR-058: All entities displayed in URLs must have a Slug
- [x] FR-059: Slug must be automatically generated from the entity english name
- [x] FR-060: Slug must be unique across the entire table
- [x] FR-061: Slug must contain only ASCII characters (English letters and numbers)
<!-- - [ ] FR-062: Slug must support automatic conversion from Persian/Farsi names to English -->
- [x] FR-063: Admin must be able to manually edit the Slug
- [x] FR-064: If Slug is duplicate, append version number (e.g., iphone-15-pro-2)

### Entities with Slug Support

| Entity          | Example URL                            | Example Slug         |
| --------------- | -------------------------------------- | -------------------- |
| Product         | `/Products/iphone-15-pro`              | `iphone-15-pro`      |
| Category        | `/Categories/smartphones`              | `smartphones`        |
| Brand           | `/Brands/apple`                        | `apple`              |
| Article         | `/Articles/top-10-smartphones`         | `top-10-smartphones` |
| ArticleCategory | `/Articles/categories/product-reviews` | `product-reviews`    |

## 13. Dynamic Role & Permission Management

### 13.1 Default Roles (System Seed)

| Role           | Description                                           | Access Level |
| -------------- | ----------------------------------------------------- | ------------ |
| Super Admin    | Full system access                                    | 100          |
| Admin          | Administrative access (except Super Admin management) | 80           |
| ContentManager | Article and comment management                        | 50           |
| Customer       | Standard user access                                  | 10           |

### 13.2 Permission Management

- [x] FR-065: System must support granular permissions (Create, Read, Update, Delete)
- [x] FR-066: Each permission must be assignable to any role
- [x] FR-067: Permissions must be grouped by module (Product, Order, User, etc.)

### 13.3 Create New Role (Super Admin only)

- [x] FR-068: Super Admin must be able to create new custom roles
- [x] FR-069: Super Admin must be able to set role name and description
- [x] FR-070: Super Admin must be able to assign permissions to new roles

### 13.4 Edit Role

- [x] FR-071: Super Admin must be able to edit role information (name, description)
- [x] FR-072: Super Admin must be able to add/remove permissions from existing roles
- [x] FR-073: Super Admin must NOT be able to delete or modify Super Admin role permissions

### 13.5 Delete Role

- [x] FR-074: Super Admin must be able to delete custom roles
- [x] FR-075: System must prevent deletion of default roles (Super Admin, Admin, ContentManager, Customer)
- [x] FR-076: System must check if role has assigned users before deletion
- [x] FR-077: System must prompt confirmation when deleting role with assigned users

### 13.6 Assign Roles to Users

- [x] FR-078: Admin/Super Admin must be able to assign multiple roles to a user
- [x] FR-079: Admin/Super Admin must be able to remove roles from users
- [x] FR-080: Admin cannot assign Super Admin role to any user

### 13.7 Permission Checking

- [x] FR-081: System must check user permissions before every operation
- [x] FR-082: System must return 403 Forbidden if user lacks required permission
- [ ] FR-083: Permissions must be cached for performance

### 13.8 Permission Groups (Modules)

| Module              | Available Permissions                                                            |
| ------------------- | -------------------------------------------------------------------------------- |
| User Management     | `users.create`, `users.read`, `users.update`, `users.delete`                     |
| Role Management     | `roles.create`, `roles.read`, `roles.update`, `roles.delete`                     |
| Product Management  | `products.create`, `products.read`, `products.update`, `products.delete`         |
| Category Management | `categories.create`, `categories.read`, `categories.update`, `categories.delete` |
| Brand Management    | `brands.create`, `brands.read`, `brands.update`, `brands.delete`                 |
| Order Management    | `orders.read`, `orders.update`, `orders.cancel`                                  |
| Comment Management  | `comments.read`, `comments.approve`, `comments.reject`, `comments.delete`        |
| Article Management  | `articles.create`, `articles.read`, `articles.update`, `articles.delete`         |
| Slider Management   | `sliders.create`, `sliders.read`, `sliders.update`, `sliders.delete`             |
| Coupon Management   | `coupons.create`, `coupons.read`, `coupons.update`, `coupons.delete`             |
| Dashboard           | `dashboard.view`                                                                 |

### 14. Subcategory Management

- [x] FR-084: The system must support multi-level category hierarchy
- [x] FR-085: Each category can have multiple subcategories
- [x] FR-086: Subcategories can have their own nested subcategories (unlimited depth)
- [x] FR-087: Display full category tree structure in the frontend
- [x] FR-088: Generate breadcrumb navigation showing the full category path
- [x] FR-089: Products can be assigned to any category (main or subcategory)
- [x] FR-090: Admin can move categories between different parent categories
- [x] FR-091: System must prevent circular references (a category cannot be its own ancestor)
- [x] FR-092: Deleting a parent category must handle subcategories (option: delete all, move to another parent, or prevent deletion)

## 15. Reporting & Invoice System

### 15.1 Invoice Management

- [ ] FR-093: System must automatically generate invoice after successful payment
- [ ] FR-094: Invoice must have unique sequential number (e.g., INV-20250001)
- [ ] FR-095: Invoice must include: order details, customer info, prices, tax, total amount
- [ ] FR-096: System must support PDF export for invoices
- [ ] FR-097: Customer must be able to download invoice from order history
- [ ] FR-098: Customer must be able to receive invoice via email
- [ ] FR-099: Admin can regenerate invoice if needed

### 15.2 Daily Sales Report

- [ ] FR-100: System must provide daily sales report
- [ ] FR-101: Daily report must include: date, order count, total sales, average order value
- [ ] FR-102: Admin must be able to filter daily report by date range
- [ ] FR-103: Admin must be able to export daily report to Excel
- [ ] FR-104: Admin must be able to export daily report to PDF

## 16. Observability & Monitoring

### 16.1 OpenTelemetry Integration

- [ ] FR-105: System must integrate OpenTelemetry SDK for metrics collection
- [ ] FR-106: System must support OTLP protocol for data export
- [ ] FR-107: System must capture HTTP request metrics (count, duration, status)
- [ ] FR-108: System must capture .NET runtime metrics (GC, memory, thread pool)
- [ ] FR-109: System must support distributed tracing with ActivitySource

### 16.2 Prometheus Integration

- [ ] FR-110: Metrics must be exposed in Prometheus format at /metrics endpoint
- [ ] FR-111: Prometheus must scrape metrics every 15 seconds
- [ ] FR-112: Custom business metrics must be defined (orders, sales, cart)

### 16.3 Grafana Dashboards

- [ ] FR-113: Grafana must be configured as visualization layer
- [ ] FR-114: Pre-configured dashboards for business KPIs must exist
- [ ] FR-115: Pre-configured dashboards for technical metrics must exist

### 16.4 Alerting

- [ ] FR-116: System must support alert rules in Prometheus
- [ ] FR-117: Alerts must be visible in Grafana
- [ ] FR-118: Critical alerts (high error rate, service down) must be configured

### 16.5 Tracing

- [ ] FR-119: Distributed tracing must be enabled for HTTP requests
- [ ] FR-120: SQL queries must be included in traces
- [ ] FR-121: Trace ID must be injected into logs for correlation

## 17. Email Verification & Security

### 17.1 Email Verification

- [x] FR-122: System must send verification email after user registration
- [x] FR-123: Verification link must contain a unique SecurityCode
- [x] FR-124: SecurityCode must expire after 1 hour
- [x] FR-125: User cannot login until email is verified
- [x] FR-126: User can request resend verification email

### 17.2 Password Recovery

- [x] FR-127: User can request password reset via email
- [x] FR-128: Password reset link must contain SecurityCode
- [x] FR-129: SecurityCode must expire after 1 hour for password reset
- [x] FR-130: User must enter new password after verification

## 18. Object Mapping (Mapster)

### 18.1 Requirements

- [x] FR-131: System must use Mapster as the primary object mapper
- [x] FR-132: Mapster must be configured in Application layer for DTO mapping
- [x] FR-133: Entity to DTO mapping must support custom field naming
- [x] FR-134: Mapping must support nested objects and collections
- [x] FR-135: ProjectToType must be used for EF Core query optimization
- [x] FR-136: Mapping configurations must be centralized in one location
- [ ] FR-137: Unit tests must cover critical mappings

### 18.2 Performance Requirements

- [x] FR-138: Mapping 10,000 objects must complete within 50ms
- [x] FR-139: Memory allocation per mapping must be minimal (< 50KB per 1000 objects)
- [x] FR-140: Cold start mapping must not exceed 100ms

## 19. Shopping Cart - Redis Migration

### 19.1 Storage Migration

- [ ] FR-141: Shopping cart data must be stored in Redis instead of SQL Server
- [ ] FR-142: Cart and CartItem entities must be removed from SQL Server
- [ ] FR-143: Redis must be configured with AOF persistence
- [ ] FR-144: Redis data must be stored on a Docker volume for durability

### 19.2 Cart Lifecycle

- [ ] FR-145: Cart must have infinite TTL (never auto-expire)
- [ ] FR-146: Cart must be manually deleted after successful checkout
- [ ] FR-147: Cart must persist across user sessions

### 19.3 Performance Requirements

- [ ] FR-148: Cart read/write operations must complete within 50ms
- [ ] FR-149: Redis must handle at least 1000 concurrent cart operations

## Non-Functional Requirements

### Security

- [x] NFR-001: Password encryption with BCrypt
- [x] NFR-002: Input validation with FluentValidation
- [ ] NFR-003: Protection against XSS, CSRF, SQL Injection

### Reliability

- [ ] NFR-006: Health Checks for service monitoring
- [ ] NFR-007: Structured logging with Serilog and Seq

### Maintainability

- [x] NFR-008: Clean Architecture implementation
- [x] NFR-009: Design patterns (Repository, UnitOfWork, CQRS, MediatR)
- [x] NFR-010: API documentation with Scalar

### Scalability

- [x] NFR-011: Containerization with Docker
- [x] NFR-012: Docker Compose for easy deployment

### Observability

- [ ] NFR-013: OpenTelemetry for distributed tracing

```

```
