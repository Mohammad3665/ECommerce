# ECommerce System Requirements

## Functional Requirements

### 1. User Management & Authentication

- [ ] FR-001: User registration with default Customer role
- [ ] FR-002: Login with JWT token
- [ ] FR-003: User creation by Admin/Owner with custom role assignment
- [ ] FR-004: User profile editing
- [ ] FR-005: Password change functionality
- [ ] FR-006: User list display for Admin/Owner
- [ ] FR-007: Enable/disable user accounts
- [ ] FR-008: Default role management (Owner, Admin, ContentManager, Customer)

### 2. Brand Management

- [ ] FR-009: Create new brand
- [ ] FR-010: Edit brand information
- [ ] FR-011: Delete brand (with product dependency check)
- [ ] FR-012: Activate/deactivate brand
- [ ] FR-013: Assign brand to products
- [ ] FR-014: Change product's brand
- [ ] FR-015: View products by brand

### 3. Category Management

- [ ] FR-016: Complete CRUD operations for categories
- [ ] FR-017: Activate/deactivate category
- [ ] FR-018: View products by category

### 4. Product Management

- [ ] FR-019: Complete CRUD operations for products
- [ ] FR-020: Multiple image upload for products
- [ ] FR-021: Product specifications and attributes
- [ ] FR-022: Inventory management
- [ ] FR-023: Low stock alert (below 5 units)
- [ ] FR-024: Activate/deactivate product

### 5. Product Search & Filtering

- [ ] FR-025: Keyword search
- [ ] FR-026: Filter by brand and category
- [ ] FR-027: Price range filter (from-to)
- [ ] FR-028: Stock filter (in-stock/out-of-stock)
- [ ] FR-029: Sorting (most expensive, cheapest, most viewed, newest)
- [ ] FR-030: Pagination

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

- [ ] FR-041: Article CRUD by Owner/Admin/ContentManager
- [ ] FR-042: Article categorization
- [ ] FR-043: Display latest articles on homepage
- [ ] FR-044: View article details

### 9. Comment System

- [ ] FR-045: Comment submission by authenticated users only
- [ ] FR-046: Nested replies to comments
- [ ] FR-047: Comment approval by ContentManager/Admin/Owner
- [ ] FR-048: Display approved comments on product page

### 10. Slider Management

- [ ] FR-049: Slider CRUD by Admin/Owner
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

- [ ] FR-058: All entities displayed in URLs must have a Slug
- [ ] FR-059: Slug must be automatically generated from the entity name
- [ ] FR-060: Slug must be unique across the entire table
- [ ] FR-061: Slug must contain only ASCII characters (English letters and numbers)
- [ ] FR-062: Slug must support automatic conversion from Persian/Farsi names to English
- [ ] FR-063: Admin must be able to manually edit the Slug
- [ ] FR-064: If Slug is duplicate, append version number (e.g., iphone-15-pro-2)

### Entities with Slug Support

| Entity          | Example URL                            | Example Slug         |
| --------------- | -------------------------------------- | -------------------- |
| Product         | `/Products/iphone-15-pro`              | `iphone-15-pro`      |
| Category        | `/Categories/smartphones`              | `smartphones`        |
| Brand           | `/Brands/apple`                        | `apple`              |
| Article         | `/Articles/top-10-smartphones`         | `top-10-smartphones` |
| ArticleCategory | `/Articles/categories/product-reviews` | `product-reviews`    |

## 13. Dynamic Role & Permission Management

### 6.1 Default Roles (System Seed)

| Role           | Description                                     | Access Level |
| -------------- | ----------------------------------------------- | ------------ |
| Owner          | Full system access                              | 100          |
| Admin          | Administrative access (except Owner management) | 80           |
| ContentManager | Article and comment management                  | 50           |
| Customer       | Standard user access                            | 10           |

### 6.2 Permission Management

- [ ] FR-065: System must support granular permissions (Create, Read, Update, Delete)
- [ ] FR-066: Each permission must be assignable to any role
- [ ] FR-067: Permissions must be grouped by module (Product, Order, User, etc.)

### 6.3 Create New Role (Owner only)

- [ ] FR-068: Owner must be able to create new custom roles
- [ ] FR-069: Owner must be able to set role name and description
- [ ] FR-070: Owner must be able to assign permissions to new roles

### 6.4 Edit Role

- [ ] FR-071: Owner must be able to edit role information (name, description)
- [ ] FR-072: Owner must be able to add/remove permissions from existing roles
- [ ] FR-073: Owner must NOT be able to delete or modify Owner role permissions

### 6.5 Delete Role

- [ ] FR-074: Owner must be able to delete custom roles
- [ ] FR-075: System must prevent deletion of default roles (Owner, Admin, ContentManager, Customer)
- [ ] FR-076: System must check if role has assigned users before deletion
- [ ] FR-077: System must prompt confirmation when deleting role with assigned users

### 6.6 Assign Roles to Users

- [ ] FR-078: Admin/Owner must be able to assign multiple roles to a user
- [ ] FR-079: Admin/Owner must be able to remove roles from users
- [ ] FR-080: Admin cannot assign Owner role to any user

### 6.7 Permission Checking

- [ ] FR-081: System must check user permissions before every operation
- [ ] FR-082: System must return 403 Forbidden if user lacks required permission
- [ ] FR-083: Permissions must be cached for performance

### 6.8 Permission Groups (Modules)

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

## Non-Functional Requirements

### Security

- [ ] NFR-001: Password encryption with BCrypt
- [ ] NFR-002: Input validation with FluentValidation
- [ ] NFR-003: Protection against XSS, CSRF, SQL Injection

### Reliability

- [ ] NFR-006: Health Checks for service monitoring
- [ ] NFR-007: Structured logging with Serilog and Seq

### Maintainability

- [ ] NFR-008: Clean Architecture implementation
- [ ] NFR-009: Design patterns (Repository, UnitOfWork, CQRS, MediatR)
- [ ] NFR-010: API documentation with Swagger

### Scalability

- [ ] NFR-011: Containerization with Docker
- [ ] NFR-012: Docker Compose for easy deployment

### Observability

- [ ] NFR-013: OpenTelemetry for distributed tracing
