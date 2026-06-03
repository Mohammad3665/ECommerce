# ECommerce System Requirements

### [x] = Completed

### [ ] = Not Completed

## Functional Requirements

### 1. User Management & Authentication

- [ ] FR-001: User registration with default Customer role
- [ ] FR-002: Login with JWT token
- [ ] FR-003: User creation by Admin/Owner with custom role assignment
- [ ] FR-004: User profile editing
- [ ] FR-005: Password change functionality
- [ ] FR-006: User list display for Admin/Owner
- [ ] FR-007: Enable/disable user accounts
- [ ] FR-008: Role management (Owner, Admin, ContentManager, Customer)

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
