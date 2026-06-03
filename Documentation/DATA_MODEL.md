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
    public bool IsActive { get; set; } = true;
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
    public Role Role { get; set; } // Owner, Admin, ContentManager, Customer
    public DateTime? LastLoginAt { get; set; }

    // Relationships
    public ICollection<Order> Orders { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public Cart Cart { get; set; }
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
    public string Logo { get; set; }

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
    public string Image { get; set; }

    // Relationships
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
    public int BrandId { get; set; }
    public int CategoryId { get; set; }

    // Relationships
    public Brand Brand { get; set; }
    public Category Category { get; set; }
    public ICollection<ProductImage> Images { get; set; }
    public ICollection<ProductSpecification> Specifications { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
```

### 6. Produact Image

```csharp
public class ProductImage
{
    public long Id { get; set; }
    public string ImageUrl { get; set; }
    public bool IsMain { get; set; }
    public int DisplayOrder { get; set; }
    public long ProductId { get; set; }
    public DateTime CreatedAt { get; set; }

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

### 8. Cart

```csharp
public class Cart : BaseEntity<long>
{
    public Guid UserId { get; set; }

    // Relationships
    public User User { get; set; }
    public ICollection<CartItem> Items { get; set; }
}
```

### 9. CartItem

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
```

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

    // Shipping information
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

    // Payment information
    public string PaymentMethod { get; set; }
    public string TransactionId { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaidAt { get; set; }

    // Relationships
    public User User { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public ICollection<OrderHistory> Histories { get; set; }
    public int? CouponId { get; set; }
    public Coupon Coupon { get; set; }

    // Ignoring the IsActive property
    [NotMapped]
    public new bool IsActive { get; set; } = true;
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
    public int? ChangedByUserId { get; set; }

    // Relationships
    public Order Order { get; set; }
    public User ChangedByUser { get; set; }
}
```

### 13.Coupon

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

    // Relationships
    public ICollection<Order> Orders { get; set; }
}

public enum CouponType
{
    Percentage,   // Percentage discount
    FixedAmount   // Fixed amount discount
}
```

### 14. Article

```csharp
public class Article : BaseEntity<long>
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Content { get; set; }
    public string Summary { get; set; }
    public string ImageUrl { get; set; }
    public int ViewCount { get; set; }
    public int ArticleCategoryId { get; set; }
    public int AuthorId { get; set; }
    public bool IsPublished { get; set; }

    // Relationships
    public ArticleCategory Category { get; set; }
    public User Author { get; set; }
}
```

### 15. ArticleCategory

```csharp
public class ArticleCategory : BaseEntity<long>
{
    public string Name { get; set; }
    public string Slug { get; set; }

    // Relationships
    public ICollection<Article> Articles { get; set; }
}
```

### 16.Comment

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

### 17. Slide

```csharp
public class Slide : BaseEntity<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Link { get; set; }
    public int DisplayOrder { get; set; }
}
```
