# Data Model

## Entities and Relationships

### 1. Base Entity

```csharp
public class BaseEntity<TKey>
{
    [Key]
    public TKey Id { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
```

### 2. User

```csharp
public class User : BaseEntity<Guid>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;

    // Security
    public Guid? SecurityCode { get; set; }
    public DateTime? SecurityCodeExpiry { get; set; }
    public bool IsEmailConfirmed { get; set; }

    // Relationships
    public ICollection<Role> UserRoles { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Comment> Comments { get; set; }
    // public Cart Cart { get; set; }
    public ICollection<Article> Articles { get; set; }

}
```

### 3. Brand

```csharp
public class Brand : BaseEntity<long>
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string LogoImageUrl { get; set; }
    public bool IsActive { get; set; } = true;

    // Relationships
    public ICollection<Product> Products { get; set; }
}
```

### 4. Cteagory

```csharp
public class Category : BaseEntity<long>
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    //Self-Reference for sub category
    public long? ParentCategoryId { get; set; }

    // Relationships
    public Category ParentCategory { get; set; }
    public ICollection<Category> SubCategpries { get; set; }
    public ICollection<Product> Products { get; set; }
}
```

### 5. Product

```csharp
public class Product : BaseEntity<long>
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int ViewCount { get; set; }

    // Foreign keys
    public long BrandId { get; set; }
    public long CategoryId { get; set; }

    // Relationships
    public Brand Brand { get; set; }
    public Category Category { get; set; }
    public ICollection<ProductImage> Images { get; set; }
    public ICollection<ProductSpecification> Specifications { get; set; }
    public ICollection<Comment> Comments { get; set; }
    // public ICollection<CartItem> CartItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
```

### 6. Produact Image

```csharp
public class ProductImage : BaseEntity<long>
{
    public string ImageUrl { get; set; }
    public bool IsMain { get; set; }
    public int DisplayOrder { get; set; }
    public long ProductId { get; set; }

    // Relationships
    public Product Product { get; set; }
}
```

### 7. ProductSpecification

```csharp
public class ProductSpecification : BaseEntity<long>
{
    public string Key { get; set; } // "Color", "Storage", "Processor"
    public string Value { get; set; } // "Red", "256GB", "Intel i7"
    public long ProductId { get; set; }

    // Relationships
    public Product Product { get; set; }
}
```

<!-- ### 8. Cart

```csharp
public class Cart : BaseEntity<long>
{
    public Guid UserId { get; set; }

    // Relationships
    public User User { get; set; }
    public ICollection<CartItem> Items { get; set; }
}
``` -->

<!-- ### 9. CartItem

```csharp
public class CartItem : BaseEntity<long>
{
    public long CartId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime AddedAt { get; set; }

    // Relationships
    public Cart Cart { get; set; }
    public Product Product { get; set; }
}
``` -->

### 10. Order

```csharp
public class Order : BaseEntity<long>
{
    public string OrderNumber { get; set; } // Unique
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid? CouponId { get; set; }

    // Relationships
    public User User { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public ICollection<OrderHistory> Histories { get; set; }
    public Coupon Coupon { get; set; }
}

public enum OrderStatus
{
    Pending,      // Awaiting payment
    Paid,         // Payment completed
    Processing,   // Processing order
    Shipping,     // Shipped
    Delivered,    // Delivered to customer
    Cancelled     // Cancelled
}
```

### 11. OrderItem

```csharp
public class OrderItem : BaseEntity<Guid>
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    // Relationships
    public Order Order { get; set; }
    public Product Product { get; set; }
}
```

### 12. OrderHistory

```csharp
public class OrderHistory : BaseEntity<long>
{
    public long OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public string Note { get; set; }
    public DateTime ChangedAt { get; set; }
    public Guid? ChangedByUserId { get; set; }

    // Relationships
    public Order Order { get; set; }
    public User ChangedByUser { get; set; }
}
```

### 13. OrderPayment

```csharp
public class OrderPayment : BaseEntity<long>
{
    public string PaymentMethod { get; set; }
    public string TransactionId { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }
}
```

### 14. OrderShipping

```csharp
public class OrderShipping : BaseEntity<long>
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
}
```

### 15. Coupon

```csharp
public class Coupon : BaseEntity<Guid>
{
    public string Code { get; set; }
    public CouponType Type { get; set; } // Percentage, FixedAmount
    public decimal Value { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public int? UsageLimit { get; set; }
    public int UsedCount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;

    // Relationships
    public ICollection<Order> Orders { get; set; }
}

public enum CouponType
{
    Percentage,   // Percentage discount
    FixedAmount   // Fixed amount discount
}
```

### 16. Article

```csharp
public class Article : BaseEntity<long>
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    public string Summary { get; set; }
    public string ImageUrl { get; set; }
    public int ViewCount { get; set; }
    public long ArticleCategoryId { get; set; }
    public Guid AuthorId { get; set; }
    public ArticleStatus Status { get; set; }

    // Relationships
    public ArticleCategory Category { get; set; }
    public User Author { get; set; }
}

public enum ArticleStatus
{
    Draft,
    Published,
    Archived
}
```

### 17. ArticleCategory

```csharp
public class ArticleCategory : BaseEntity<long>
{
    public string Name { get; set; }
    public string Slug { get; set; }

    // Relationships
    public ICollection<Article> Articles { get; set; }
}
```

### 18.Comment

```csharp
public class Comment : BaseEntity<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public long ProductId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public bool IsApproved { get; set; }
    public DateTime? ApprovedAt { get; set; }

    // Relationships
    public User User { get; set; }
    public Product Product { get; set; }
    public Comment ParentComment { get; set; }
    public ICollection<Comment> Replies { get; set; }
}
```

### 19. Slide

```csharp
public class Slide : BaseEntity<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Link { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
```

### 20. Role

```csharp
public class Role : BaseEntity<long>
{
    public string Name { get; set; }           // "ProductManager"
    public string DisplayName { get; set; }    // "مدیر محصولات"
    public string Description { get; set; }
    public bool IsDefault { get; set; }        // true for Super Admin, Admin, ContentManager, Customer
    public bool IsSystemProtected { get; set; } // Cannot delete/modify Super Admin role
    public int Level { get; set; }              // 100=Super Admin, 80=Admin, 50=ContentManager, 10=Customer

    // Relationships
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}
```

### 21. Permission

```csharp
public class Permission : BaseEntity<long>
{
    public string Name { get; set; }           // "products.create"
    public string Module { get; set; }         // "Products"
    public string Description { get; set; }

    // Relationships
    public ICollection<RolePermission> RolePermissions { get; set; }
}

```

### 22. User Role (Many To Many)

```csharp
public class UserRole
{
    public Guid UserId { get; set; }
    public long RoleId { get; set; }
    public DateTime AssignedAt { get; set; }
    public Guid? AssignedByUserId { get; set; }

    // Relationships
    public User User { get; set; }
    public Role Role { get; set; }
    public User AssignedBy { get; set; }
}

```

### 23. Role Permission (Many To Many)

```csharp
public class RolePermission
{
    public long RoleId { get; set; }
    public long PermissionId { get; set; }

    // Relationships
    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
```

### 24. Invoice

```csharp
public class Invoice : BaseEntity<long>
{
    public long OrderId { get; set; }
    public string InvoiceNumber { get; set; }        // INV-20250001
    public DateTime InvoiceDate { get; set; }

    // Financial details
    public decimal SubTotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }           // 9% VAT
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }

    // Document
    public string InvoicePdfUrl { get; set; }

    // Status
    public InvoiceStatus Status { get; set; }

    // Relationships
    public Order Order { get; set; }
}

public enum InvoiceStatus
{
    Issued,      // Invoice created
    Paid,        // Payment confirmed
    Cancelled    // Invoice cancelled
}
```

### 25. Daly Sales Report (DTO)

```csharp
public class DailySalesReportDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalSales { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalShipping { get; set; }
    public decimal AverageOrderValue { get; set; }
    public List<DailySalesItemDto> DailyItems { get; set; }
}
```

### 26. Daily Sales Item (DTO)

```csharp
public class DailySalesItemDto
{
    public DateTime Date { get; set; }
    public int OrderCount { get; set; }
    public decimal TotalSales { get; set; }
    public decimal TotalTax { get; set; }
    public decimal TotalShipping { get; set; }
    public decimal AverageOrderValue { get; set; }
}
```

### 27. Redis Cart (DTO)
```csharp
public class RedisCartDto
{
    public Guid UserId { get; set; }
    public List<RedisCartItemDto> Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal TotalPrice { get; set; }
}
```

### 28. Redis Cart Item (DTO)
```csharp
public class RedisCartItemDto
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => Quantity * UnitPrice;
}
```

### 29. Initial Seeding

```csharp
public static class Permissions
{
    // User Permissions
    public const string UsersCreate = "users.create";
    public const string UsersRead = "users.read";
    public const string UsersUpdate = "users.update";
    public const string UsersDelete = "users.delete";

    // Role Permissions
    public const string RolesCreate = "roles.create";
    public const string RolesRead = "roles.read";
    public const string RolesUpdate = "roles.update";
    public const string RolesDelete = "roles.delete";

    // Product Permissions
    public const string ProductsCreate = "products.create";
    public const string ProductsRead = "products.read";
    public const string ProductsUpdate = "products.update";
    public const string ProductsDelete = "products.delete";

    // Category Permissions
    public const string CategoriesCreate = "categories.create";
    public const string CategoriesRead = "categories.read";
    public const string CategoriesUpdate = "categories.update";
    public const string CategoriesDelete = "categories.delete";

    // Brand Permissions
    public const string BrandsCreate = "brands.create";
    public const string BrandsRead = "brands.read";
    public const string BrandsUpdate = "brands.update";
    public const string BrandsDelete = "brands.delete";

    // Order Permissions
    public const string OrdersRead = "orders.read";
    public const string OrdersUpdate = "orders.update";
    public const string OrdersCancel = "orders.cancel";

    // Comment Permissions
    public const string CommentsRead = "comments.read";
    public const string CommentsApprove = "comments.approve";
    public const string CommentsReject = "comments.reject";
    public const string CommentsDelete = "comments.delete";

    // Article Permissions
    public const string ArticlesCreate = "articles.create";
    public const string ArticlesRead = "articles.read";
    public const string ArticlesUpdate = "articles.update";
    public const string ArticlesDelete = "articles.delete";

    // Slider Permissions
    public const string SlidersCreate = "sliders.create";
    public const string SlidersRead = "sliders.read";
    public const string SlidersUpdate = "sliders.update";
    public const string SlidersDelete = "sliders.delete";

    // Coupon Permissions
    public const string CouponsCreate = "coupons.create";
    public const string CouponsRead = "coupons.read";
    public const string CouponsUpdate = "coupons.update";
    public const string CouponsDelete = "coupons.delete";

    // Dashboard Permissions
    public const string DashboardView = "dashboard.view";
}
```

### 30. Seed Data Configuration

```csharp
public static class DefaultRoles
{
    public static readonly List<Role> Roles = new()
    {
        new Role { Id = 1, Name = "SuperAdmin", DisplayName = "مدیر کل", IsDefault = true, IsSystemProtected = true, Level = 100 },
        new Role { Id = 2, Name = "Admin", DisplayName = "مدیر", IsDefault = true, IsSystemProtected = true, Level = 80 },
        new Role { Id = 3, Name = "ContentManager", DisplayName = "مدیر محتوا", IsDefault = true, IsSystemProtected = true, Level = 50 },
        new Role { Id = 4, Name = "Customer", DisplayName = "مشتری", IsDefault = true, IsSystemProtected = true, Level = 10 }
    };
}
```
