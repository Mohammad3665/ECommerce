# API Specification - ECommerce

## Base URL

Development: https://localhost:5000/api/v1

## 1. Authentication

**Base Path:** `/api/v1/Auth`

| Method | Endpoint                | Description         | Access    |
| ------ | ----------------------- | ------------------- | --------- |
| POST   | `/Auth/Register`        | Register new user   | Public    |
| POST   | `/Auth/Login`           | Login and get token | Public    |
| POST   | `/Auth/Change-password` | Change password     | Customer+ |
| POST   | `/Auth/Logout`          | Logout              | Customer+ |

**Sample Request (Login):**

```json
{
  "email": "user@example.com",
  "password": "MyPassword123"
}
```

**Sample Response (Login):**

```json
{
  "isSuccess": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
    "expiration": "2025-12-31T23:59:59",
    "fullName": "John Doe",
    "role": "Customer"
  }
}
```

## 2. User Management

**Base Path:** `/api/v1/Admin/Users`

| Method | Endpoint                   | Description               | Access             |
| ------ | -------------------------- | ------------------------- | ------------------ |
| GET    | `/Admin/Users`             | Get all users (paginated) | Admin, Super Admin |
| GET    | `/Admin/Users/{id}`        | Get user by id            | Admin, Super Admin |
| POST   | `/Admin/Users`             | Create user               | Admin, Super Admin |
| PUT    | `/Admin/Users/{id}`        | Update user               | Admin, Super Admin |
| DELETE | `/Admin/Users/{id}`        | Delete user               | Super Admin only   |
| PATCH  | `/Admin/Users/{id}/Status` | Activate/deactivate user  | Admin, Super Admin |
| PATCH  | `/Admin/Users/{id}/Role`   | Change user role          | Super Admin only   |

**Sample Request (Create User):**

```json
{
  "fullName": "Jane Smith",
  "email": "jane@example.com",
  "phoneNumber": "09123456780",
  "password": "TempP@ss123",
  "role": "ContentManager"
}
```

**Sample Response (Get Users):**

```json
{
  "isSuccess": true,
  "data": {
    "items": [
      {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "fullName": "John Doe",
        "email": "john@example.com",
        "role": "Customer",
        "isActive": true,
        "createdAt": "2025-01-01T10:00:00Z"
      }
    ],
    "totalCount": 50,
    "pageNumber": 1,
    "pageSize": 20
  }
}
```

## 3. Profile

**Base Path:** `/api/v1/Profile`

| Method | Endpoint            | Description       | Access    |
| ------ | ------------------- | ----------------- | --------- |
| GET    | `/Profile`          | Get my profile    | Customer+ |
| PUT    | `/Profile`          | Update my profile | Customer+ |
| GET    | `/Profile/Orders`   | Get my orders     | Customer+ |
| GET    | `/Profile/Comments` | Get my comments   | Customer+ |

**Sample Request (Update Profile):**

```json
{
  "fullName": "John Doe Updated",
  "phoneNumber": "09123456780"
}
```

**Sample Response (Get Profile):**

```json
{
  "isSuccess": true,
  "data": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "fullName": "John Doe",
    "email": "john@example.com",
    "phoneNumber": "09123456789",
    "role": "Customer",
    "createdAt": "2025-01-01T10:00:00Z"
  }
}
```

## 4. Brands

| Method | Endpoint                    | Description               | Access             |
| ------ | --------------------------- | ------------------------- | ------------------ |
| GET    | `/Brands`                   | Get all brands            | Public             |
| GET    | `/Brands/{id}`              | Get brand by id           | Public             |
| GET    | `/Brands/{slug}`            | Get brand by slug         | Public             |
| GET    | `/Brands/{id}/Products`     | Get products by brand     | Public             |
| POST   | `/Admin/Brands`             | Create brand              | Admin, Super Admin |
| PUT    | `/Admin/Brands/{id}`        | Update brand              | Admin, Super Admin |
| DELETE | `/Admin/Brands/{id}`        | Delete brand              | Admin, Super Admin |
| PATCH  | `/Admin/Brands/{id}/Status` | Activate/deactivate brand | Admin, Super Admin |

**Sample Request (Create Brand):**

```json
{
  "name": "Apple",
  "description": "American technology company",
  "logo": "/uploads/logos/apple.png"
}
```

**Sample Response (Get Brands):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "name": "Apple",
      "slug": "apple",
      "description": "American technology company",
      "logo": "/uploads/logos/apple.png",
      "isActive": true,
      "productCount": 25
    }
  ]
}
```

## 5. Categories

| Method | Endpoint                         | Description                                                     | Access             |
| ------ | -------------------------------- | --------------------------------------------------------------- | ------------------ |
| GET    | `/Categories`                    | Get all categories                                              | Public             |
| GET    | `/Categories/{id}`               | Get category by id                                              | Public             |
| GET    | `/Categories/{slug}`             | Get category by slug                                            | Public             |
| GET    | `/Categories/{id}/Products`      | Get products by category                                        | Public             |
| POST   | `/Admin/Categories`              | Create category                                                 | Admin, Super Admin |
| PUT    | `/Admin/Categories/{id}`         | Update category                                                 | Admin, Super Admin |
| DELETE | `/Admin/Categories/{id}`         | Delete category                                                 | Admin, Super Admin |
| PATCH  | `/Admin/Categories/{id}/Status`  | Activate/deactivate category                                    | Admin, Super Admin |
| GET    | `/Categories/Tree`               | Get full category tree with all nested subcategories            | Public             |
| GET    | `/categories/{id}/Subcategories` | Get direct subcategories of a category                          | Public             |
| GET    | `/Categories/{id}/Full-Path`     | Get breadcrumb path (e.g., Electronics > Mobiles > Smartphones) | Public             |
| GET    | `/Categories/{id}/Descendants`   | Get all descendant categories (all levels below)                | Public             |
| GET    | `/Categories/{id}/Ancestors`     | Get all ancestor categories (parents chain)                     | Public             |
| POST   | `/Admin/Categories/{id}/Move`    | Move category to another parent category                        | Admin, Super Admin |

**Sample Request (Create Category):**

```json
{
  "name": "Smartphones",
  "description": "Mobile phones and accessories",
  "image": "/uploads/categories/smartphones.jpg"
}
```

**Sample Response (Get Categories):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "name": "Smartphones",
      "slug": "smartphones",
      "description": "Mobile phones and accessories",
      "image": "/uploads/categories/smartphones.jpg",
      "isActive": true,
      "productCount": 45
    }
  ]
}
```

**Sample Response (Get Categories Tree):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "name": "Electronics",
      "slug": "electronics",
      "level": 0,
      "subCategories": [
        {
          "id": 2,
          "name": "Mobiles",
          "slug": "mobiles",
          "level": 1,
          "subCategories": [
            {
              "id": 4,
              "name": "Smartphones",
              "slug": "smartphones",
              "level": 2,
              "subCategories": []
            }
          ]
        }
      ]
    }
  ]
}
```

## 6. Products

| Method | Endpoint                                                | Description                 | Access             |
| ------ | ------------------------------------------------------- | --------------------------- | ------------------ |
| GET    | `/Products`                                             | Get products with filters   | Public             |
| GET    | `/Products/{id}`                                        | Get product by id           | Public             |
| GET    | `/Products/{slug}`                                      | Get product by slug         | Public             |
| GET    | `/Products/{id}/Comments`                               | Get product comments        | Public             |
| POST   | `/Admin/Products`                                       | Create product              | Admin, Super Admin |
| PUT    | `/Admin/Products/{id}`                                  | Update product              | Admin, Super Admin |
| DELETE | `/Admin/Products/{id}`                                  | Delete product              | Admin, Super Admin |
| PATCH  | `/Admin/Products/{id}/Status`                           | Activate/deactivate product | Admin, Super Admin |
| POST   | `/Admin/Products/{id}/Images`                           | Upload product images       | Admin, Super Admin |
| DELETE | `/Admin/Products/{productId}/Images/{imageId}`          | Delete product image        | Admin, Super Admin |
| PATCH  | `/Admin/Products/{productId}/Images/{imageId}/Set-main` | Set main image              | Admin, Super Admin |
| POST   | `/Admin/Products/{id}/Specifications`                   | Add specifications          | Admin, Super Admin |
| PUT    | `/Admin/Products/{productId}/Specifications/{specId}`   | Update specification        | Admin, Super Admin |
| DELETE | `/Admin/Products/{productId}/Specifications/{specId}`   | Delete specification        | Admin, Super Admin |

**Sample Request (Create Product):**

```json
{
  "name": "iPhone 15 Pro",
  "description": "The iPhone 15 Pro features A17 chip",
  "shortDescription": "Latest Apple smartphone",
  "price": 50000000,
  "stockQuantity": 100,
  "brandId": 1,
  "categoryId": 1
}
```

**Sample Response (Get Product Details):**

```json
{
  "isSuccess": true,
  "data": {
    "id": 10,
    "name": "iPhone 15 Pro",
    "slug": "iphone-15-pro",
    "description": "The iPhone 15 Pro features A17 chip",
    "price": 50000000,
    "stockQuantity": 15,
    "viewCount": 1250,
    "brand": {
      "id": 1,
      "name": "Apple",
      "slug": "apple"
    },
    "category": {
      "id": 1,
      "name": "Smartphones",
      "slug": "smartphones"
    },
    "images": [
      {
        "id": 1,
        "imageUrl": "/uploads/products/iphone15/main.jpg",
        "isMain": true
      }
    ],
    "specifications": [
      {
        "key": "Processor",
        "value": "A17 Pro"
      }
    ]
  }
}
```

## 7. Shopping Cart

| Method | Endpoint                   | Description             | Access    |
| ------ | -------------------------- | ----------------------- | --------- |
| GET    | `/Cart`                    | Get my cart             | Customer+ |
| POST   | `/Cart/Items`              | Add item to cart        | Customer+ |
| PUT    | `/Cart/Items/{cartItemId}` | Update item quantity    | Customer+ |
| DELETE | `/Cart/Items/{cartItemId}` | Remove item from cart   | Customer+ |
| DELETE | `/Cart/Clear`              | Clear cart              | Customer+ |
| POST   | `/Cart/Apply-coupon`       | Apply coupon to cart    | Customer+ |
| DELETE | `/Cart/Remove-coupon`      | Remove coupon from cart | Customer+ |

**Sample Request (Add To Cart):**

```json
{
  "productId": 10,
  "quantity": 2
}
```

**Sample Response (Get Cart):**

```json
{
  "isSuccess": true,
  "data": {
    "items": [
      {
        "id": 1,
        "productId": 10,
        "productName": "iPhone 15 Pro",
        "productSlug": "iphone-15-pro",
        "quantity": 2,
        "unitPrice": 50000000,
        "totalPrice": 100000000,
        "imageUrl": "/uploads/products/iphone15/main.jpg"
      }
    ],
    "subTotal": 100000000,
    "discountAmount": 0,
    "shippingCost": 50000,
    "totalAmount": 100050000,
    "itemsCount": 1
  }
}
```

**Sample Request (Apply Coupon):**

```json
{
  "couponCode": "SAVE20"
}
```

**Sample Response (Apply Coupon):**

```json
{
  "isSuccess": true,
  "data": {
    "couponCode": "SAVE20",
    "discountAmount": 20000000,
    "newTotal": 80050000,
    "message": "Coupon applied successfully"
  }
}
```

## 8. Orders

| Method | Endpoint                    | Description               | Access             |
| ------ | --------------------------- | ------------------------- | ------------------ |
| POST   | `/Orders`                   | Place new order           | Customer+          |
| GET    | `/Orders`                   | Get my orders             | Customer+          |
| GET    | `/Orders/{id}`              | Get order details         | Customer+          |
| GET    | `/Admin/Orders`             | Get all orders            | Admin, Super Admin |
| GET    | `/Admin/Orders/{id}`        | Get order details (admin) | Admin, Super Admin |
| PUT    | `/Admin/Orders/{id}/Status` | Update order status       | Admin, Super Admin |
| PUT    | `/Admin/Orders/{id}/Cancel` | Cancel order              | Admin, Super Admin |

**Sample Request (Place Order):**

```json
{
  "fullName": "John Doe",
  "phoneNumber": "09123456789",
  "address": "Tehran, Iran, Address line 1",
  "postalCode": "1234567890",
  "shippingMethod": "Post",
  "paymentMethod": "SimulatedGateway"
}
```

**Sample Response (Place Order):**

```json
{
  "isSuccess": true,
  "data": {
    "orderId": 100,
    "orderNumber": "ORD-20250001",
    "totalAmount": 100050000,
    "paymentUrl": "https://sandbox.payment.com/pay/TOKEN123",
    "message": "Order created successfully. Proceed to payment."
  }
}
```

**Sample Response (Get Order Details):**

```json
{
  "isSuccess": true,
  "data": {
    "id": 100,
    "orderNumber": "ORD-20250001",
    "orderDate": "2025-01-10T15:30:00Z",
    "status": "Paid",
    "subTotal": 100000000,
    "discountAmount": 0,
    "shippingCost": 50000,
    "totalAmount": 100050000,
    "items": [
      {
        "productId": 10,
        "productName": "iPhone 15 Pro",
        "quantity": 2,
        "unitPrice": 50000000,
        "totalPrice": 100000000
      }
    ],
    "shippingInfo": {
      "fullName": "John Doe",
      "phoneNumber": "09123456789",
      "address": "Tehran, Iran",
      "postalCode": "1234567890"
    }
  }
}
```

## 9. Payment

| Method | Endpoint                    | Description               | Access    |
| ------ | --------------------------- | ------------------------- | --------- |
| POST   | `/Payment/Verify`           | Verify payment (callback) | Public    |
| GET    | `/Payment/Status/{orderId}` | Check payment status      | Customer+ |

**Sample Request (Veify Payment):**

```json
{
  "orderId": 100,
  "paymentToken": "TOKEN123",
  "paymentStatus": "success"
}
```

**Sample Response (Verify Payment):**

```json
{
  "isSuccess": true,
  "data": {
    "orderId": 100,
    "orderNumber": "ORD-20250001",
    "trackingCode": "TRK-123456",
    "message": "Payment verified successfully"
  }
}
```

## 10. Coupons

**Base Path:** `/api/v1/Admin/Coupons`

| Method | Endpoint              | Description      | Access             |
| ------ | --------------------- | ---------------- | ------------------ |
| GET    | `/Admin/Coupons`      | Get all coupons  | Admin, Super Admin |
| GET    | `/Admin/Coupons/{id}` | Get coupon by id | Admin, Super Admin |
| POST   | `/Admin/Coupons`      | Create coupon    | Admin, Super Admin |
| PUT    | `/Admin/Coupons/{id}` | Update coupon    | Admin, Super Admin |
| DELETE | `/Admin/Coupons/{id}` | Delete coupon    | Admin, Super Admin |

**Sample Request (Create Coupon):**

```json
{
  "code": "SAVE20",
  "type": "Percentage",
  "value": 20,
  "minOrderAmount": 100000,
  "usageLimit": 100,
  "startDate": "2025-01-01T00:00:00Z",
  "endDate": "2025-12-31T23:59:59Z"
}
```

**Sample Response (Get Coupons):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "code": "SAVE20",
      "type": "Percentage",
      "value": 20,
      "usedCount": 45,
      "isActive": true,
      "startDate": "2025-01-01T00:00:00Z",
      "endDate": "2025-12-31T23:59:59Z"
    }
  ]
}
```

## 11. Comments

| Method | Endpoint                         | Description            | Access          |
| ------ | -------------------------------- | ---------------------- | --------------- |
| POST   | `/Products/{productId}/Comments` | Add comment to product | Customer+       |
| PUT    | `/Comments/{id}`                 | Edit my comment        | Customer+       |
| DELETE | `/Comments/{id}`                 | Delete my comment      | Customer+       |
| GET    | `/Admin/Comments/Pending`        | Get pending comments   | ContentManager+ |
| GET    | `/Admin/Comments/Approved`       | Get approved comments  | ContentManager+ |
| GET    | `/Admin/Comments/Rejected`       | Get rejected comments  | ContentManager+ |
| PUT    | `/Admin/Comments/{id}/Approve`   | Approve comment        | ContentManager+ |
| PUT    | `/Admin/Comments/{id}/Reject`    | Reject comment         | ContentManager+ |

**Sample Request (Add Comment):**

```json
{
  "title": "Great product!",
  "content": "Very satisfied with the quality",
  "parentCommentId": null
}
```

**Sample Response (Get Pending Comments):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "title": "Great product!",
      "content": "Very satisfied with the quality",
      "userName": "John Doe",
      "productName": "iPhone 15 Pro",
      "createdAt": "2025-01-05T12:00:00Z"
    }
  ]
}
```

## 12. Articles

| Method | Endpoint                             | Description                   | Access          |
| ------ | ------------------------------------ | ----------------------------- | --------------- |
| GET    | `/Articles`                          | Get all articles              | Public          |
| GET    | `/Articles/{id}`                     | Get article by id             | Public          |
| GET    | `/Articles/{slug}`                   | Get article by slug           | Public          |
| GET    | `/Articles/Latest`                   | Get latest articles (5 items) | Public          |
| GET    | `/Articles/Categories`               | Get article categories        | Public          |
| GET    | `/Articles/Categories/{id}/Articles` | Get articles by category      | Public          |
| POST   | `/Admin/Articles`                    | Create article                | ContentManager+ |
| PUT    | `/Admin/Articles/{id}`               | Update article                | ContentManager+ |
| DELETE | `/Admin/Articles/{id}`               | Delete article                | ContentManager+ |
| POST   | `/Admin/Articles/Categories`         | Create article category       | ContentManager+ |
| PUT    | `/Admin/Articles/Categories/{id}`    | Update article category       | ContentManager+ |
| DELETE | `/Admin/Articles/Categories/{id}`    | Delete article category       | ContentManager+ |

**Sample Request (Create Article):**

```json
{
  "title": "Top 10 Smartphones of 2025",
  "content": "<p>Detailed article content...</p>",
  "summary": "Brief summary of the article",
  "articleCategoryId": 1,
  "imageUrl": "/uploads/articles/top-phones.jpg"
}
```

**Sample Response (Get Articles):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "title": "Top 10 Smartphones of 2025",
      "slug": "top-10-smartphones-2025",
      "summary": "Brief summary",
      "imageUrl": "/uploads/articles/top-phones.jpg",
      "viewCount": 1250,
      "authorName": "Jane Smith",
      "createdAt": "2025-01-10T08:00:00Z"
    }
  ]
}
```

## 13. Sliders

| Method | Endpoint                     | Description                | Access             |
| ------ | ---------------------------- | -------------------------- | ------------------ |
| GET    | `/Sliders`                   | Get all active sliders     | Public             |
| GET    | `/Admin/Sliders`             | Get all sliders (admin)    | Admin, Super Admin |
| GET    | `/Admin/Sliders/{id}`        | Get slider by id           | Admin, Super Admin |
| POST   | `/Admin/Sliders`             | Create slider              | Admin, Super Admin |
| PUT    | `/Admin/Sliders/{id}`        | Update slider              | Admin, Super Admin |
| DELETE | `/Admin/Sliders/{id}`        | Delete slider              | Admin, Super Admin |
| PATCH  | `/Admin/Sliders/{id}/Status` | Activate/deactivate slider | Admin, Super Admin |

**Sample Response (Create Slider):**

```json
{
  "title": "Summer Sale",
  "description": "Up to 50% discount on all products",
  "imageUrl": "/uploads/sliders/summer-sale.jpg",
  "link": "/products?discount=true",
  "displayOrder": 1
}
```

**Sample Response (Get Sliders):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "title": "Summer Sale",
      "description": "Up to 50% discount",
      "imageUrl": "/uploads/sliders/summer-sale.jpg",
      "link": "/products?discount=true",
      "displayOrder": 1,
      "isActive": true
    }
  ]
}
```

## 14. Dashboard

**Base Path:** `/api/v1/Admin/Dashboard`
| Method | Endpoint | Description | Access |
|----------|----------------------------------------|-----------------------------------|---------------------|
| GET | `/Admin/Dashboard/Statistics` | Get overall statistics | Admin, Super Admin |
| GET | `/Admin/Dashboard/Sales` | Get sales report | Admin, Super Admin |
| GET | `/Admin/Dashboard/Top-Products` | Get most viewed products | Admin, Super Admin |
| GET | `/Admin/Dashboard/Orders-Status` | Get orders by status | Admin, Super Admin |

**Sample Response (Dashboard Statistics):**

```json
{
  "isSuccess": true,
  "data": {
    "totalSales": 1250000000,
    "totalOrders": 450,
    "totalProducts": 125,
    "totalUsers": 890,
    "pendingOrders": 12,
    "lowStockProducts": 5,
    "totalComments": 340,
    "pendingComments": 15
  }
}
```

**Sample Response (Sales Report):**

```json
{
  "isSuccess": true,
  "data": {
    "daily": [
      { "date": "2025-01-01", "sales": 50000000, "orders": 5 },
      { "date": "2025-01-02", "sales": 75000000, "orders": 8 }
    ],
    "weekly": 350000000,
    "monthly": 1250000000,
    "yearly": 1250000000
  }
}
```

## 15. Health & Monitoring

| Method | Endpoint        | Description           | Access   |
| ------ | --------------- | --------------------- | -------- |
| GET    | `/Health/Ready` | Readiness probe       | Public   |
| GET    | `/Health/Live`  | Liveness probe        | Public   |
| GET    | `/Metrics`      | OpenTelemetry metrics | Internal |

**Sample Response (Health Check):**

```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.1234567",
  "entries": {
    "database": {
      "data": {},
      "duration": "00:00:00.0500000",
      "status": "Healthy",
      "tags": []
    },
    "seq": {
      "data": {},
      "duration": "00:00:00.0300000",
      "status": "Healthy",
      "tags": []
    }
  }
}
```

## Slug Support

| Entity   | Example URL                           |
| -------- | ------------------------------------- |
| Product  | `/api/v1/Products/iphone-15-pro`      |
| Category | `/api/v1/Categories/smartphones`      |
| Brand    | `/api/v1/Brands/apple`                |
| Article  | `/api/v1/Articles/top-10-smartphones` |

## 16. Role Management (Super Admin only)

**Base Path:** `/api/v1/SuperAdmin/Roles`

| Method | Endpoint                                            | Description                 | Access           |
| ------ | --------------------------------------------------- | --------------------------- | ---------------- |
| GET    | `/SuperAdmin/Roles`                                 | Get all roles               | Super Admin only |
| GET    | `/SuperAdmin/Roles/{id}`                            | Get role by id              | Super Admin only |
| POST   | `/SuperAdmin/Roles`                                 | Create new role             | Super Admin only |
| PUT    | `/SuperAdmin/Roles/{id}`                            | Update role                 | Super Admin only |
| DELETE | `/SuperAdmin/Roles/{id}`                            | Delete role                 | Super Admin only |
| GET    | `/SuperAdmin/Roles/{id}/Permissions`                | Get role permissions        | Super Admin only |
| POST   | `/SuperAdmin/Roles/{id}/Permissions`                | Assign permissions to role  | Super Admin only |
| DELETE | `/SuperAdmin/Roles/{id}/Permissions/{permissionId}` | Remove permission from role | Super Admin only |
| GET    | `/SuperAdmin/Roles/{id}/Users`                      | Get users with this role    | Super Admin only |

**Sample Request (Create Role):**

```json
{
  "name": "ProductManager",
  "displayName": "مدیر محصولات",
  "description": "Can manage products only",
  "level": 60,
  "permissions": [
    "products.create",
    "products.read",
    "products.update",
    "products.delete",
    "categories.read",
    "brands.read"
  ]
}
```

**Sample Response (Get Roles):**

```json
{
  "isSuccess": true,
  "data": [
    {
      "id": 1,
      "name": "SuperAdmin",
      "displayName": "مدیر کل",
      "description": "Full system access",
      "isDefault": true,
      "isSystemProtected": true,
      "level": 100,
      "userCount": 1
    },
    {
      "id": 2,
      "name": "Admin",
      "displayName": "مدیر",
      "isDefault": true,
      "isSystemProtected": true,
      "level": 80,
      "userCount": 3
    },
    {
      "id": 5,
      "name": "ProductManager",
      "displayName": "مدیر محصولات",
      "isDefault": false,
      "isSystemProtected": false,
      "level": 60,
      "userCount": 2
    }
  ]
}
```

**Sample Response (Get Role Permissions):**

```json
{
  "isSuccess": true,
  "data": {
    "roleId": 5,
    "roleName": "ProductManager",
    "permissions": [
      {
        "id": 10,
        "name": "products.create",
        "module": "Products",
        "description": "Create new products"
      },
      {
        "id": 11,
        "name": "products.read",
        "module": "Products",
        "description": "View products"
      }
    ]
  }
}
```

## 17. Permission Management (Super Admin Only)

**Base Path:** `/api/v1/SuperAdmin/Permissions`

| Method | Endpoint                          | Description                             | Access           |
| ------ | --------------------------------- | --------------------------------------- | ---------------- |
| GET    | `/SuperAdmin/Permissions`         | Get all permissions (grouped by module) | Super Admin only |
| GET    | `/SuperAdmin/Permissions/Modules` | Get all permission modules              | Super Admin only |

**Sample Response (Get Permissions):**

```json
{
  "isSuccess": true,
  "data": {
    "modules": [
      {
        "name": "User Management",
        "permissions": [
          { "name": "users.create", "description": "Create users" },
          { "name": "users.read", "description": "View users" },
          { "name": "users.update", "description": "Update users" },
          { "name": "users.delete", "description": "Delete users" }
        ]
      },
      {
        "name": "Product Management",
        "permissions": [
          { "name": "products.create", "description": "Create products" },
          { "name": "products.read", "description": "View products" },
          { "name": "products.update", "description": "Update products" },
          { "name": "products.delete", "description": "Delete products" }
        ]
      }
    ]
  }
}
```

## 18. Assign Roles to Users (Admin/Super Admin)

**Base Path:** `/api/v1/Admin/Users`

| Method | Endpoint                               | Description           | Access             |
| ------ | -------------------------------------- | --------------------- | ------------------ |
| POST   | `/Admin/Users/{userId}/Roles`          | Assign roles to user  | Admin, Super Admin |
| GET    | `/Admin/Users/{userId}/Roles`          | Get user roles        | Admin, Super Admin |
| DELETE | `/Admin/Users/{userId}/Roles/{roleId}` | Remove role from user | Admin, Super Admin |

**Sample Request (Assign Roles):**

```json
{
  "roleIds": [2, 5]
}
```

**Sample Response (Get User Roles):**

```json
{
  "isSuccess": true,
  "data": {
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "userName": "John Doe",
    "roles": [
      {
        "id": 2,
        "name": "Admin",
        "displayName": "مدیر"
      },
      {
        "id": 5,
        "name": "ProductManager",
        "displayName": "مدیر محصولات"
      }
    ]
  }
}
```

## 19. Assign Multiple Roles to User

| Method | Endpoint                               | Description                       | Access             |
| ------ | -------------------------------------- | --------------------------------- | ------------------ |
| POST   | `/Admin/Users/{userId}/Roles`          | Assign multiple roles to user     | Admin, Super Admin |
| GET    | `/Admin/Users/{userId}/Roles`          | Get all roles of a user           | Admin, Super Admin |
| DELETE | `/Admin/Users/{userId}/Roles/{roleId}` | Remove a role from user           | Admin, Super Admin |
| PUT    | `/Admin/Users/{userId}/Roles`          | Replace all roles (set new roles) | Admin, Super Admin |

**Sample Request (Assign Multiple Roles):**

```json
{
  "roleIds": [2, 5, 7] // Admin, ProductManager, CommentModerator
}
```

## 20. Invoices

**Base Path:** `/api/v1/invoices`

| Method | Endpoint                       | Description                    | Access             |
| ------ | ------------------------------ | ------------------------------ | ------------------ |
| GET    | `/Invoices/{orderId}`          | Get invoice by order ID        | Customer+          |
| GET    | `/Invoices/{id}/Pdf`           | Download invoice as PDF        | Customer+          |
| POST   | `/Invoices/{orderId}/Generate` | Generate invoice for order     | Admin, Super Admin |
| POST   | `/Invoices/{id}/Send-email`    | Send invoice to customer email | Customer+          |
| GET    | `/Admin/Invoices`              | Get all invoices               | Admin, Super Admin |

**Sample Response (Get Invoice):**

```json
{
  "isSuccess": true,
  "data": {
    "id": 1,
    "invoiceNumber": "INV-20250001",
    "invoiceDate": "2025-01-15T10:30:00Z",
    "subTotal": 1000000,
    "discountAmount": 100000,
    "taxAmount": 81000,
    "shippingCost": 50000,
    "totalAmount": 1031000,
    "status": "Paid",
    "invoicePdfUrl": "/storage/invoices/INV-20250001.pdf",
    "orderId": 100
  }
}
```

## Daily Sales Report

**Base Path:** `/api/v1/Admin/Reports`

| Method | Endpoint                                  | Description                  | Access       |
| ------ | ----------------------------------------- | ---------------------------- | ------------ |
| GET    | `/Admin/Reports/Sales/Daily`              | Get daily sales report       | Admin, Owner |
| GET    | `/Admin/Reports/Sales/Daily/Export/Excel` | Export daily report to Excel | Admin, Owner |
| GET    | `/Admin/Reports/Sales/Daily/Export/Pdf`   | Export daily report to PDF   | Admin, Owner |

**Sample Request:**
GET `/admin/reports/sales/daily?startDate=2025-01-01&endDate=2025-01-31&sortBy=totalSales&sortDirection=desc`

**Sample Response:**

```json
{
  "isSuccess": true,
  "data": {
    "startDate": "2025-01-01",
    "endDate": "2025-01-31",
    "totalOrders": 45,
    "totalSales": 125000000,
    "totalTax": 11250000,
    "totalShipping": 2250000,
    "averageOrderValue": 2777777,
    "dailyItems": [
      {
        "date": "2025-01-01",
        "orderCount": 5,
        "totalSales": 15000000,
        "totalTax": 1350000,
        "totalShipping": 250000,
        "averageOrderValue": 3000000
      },
      {
        "date": "2025-01-02",
        "orderCount": 3,
        "totalSales": 9000000,
        "totalTax": 810000,
        "totalShipping": 150000,
        "averageOrderValue": 3000000
      }
    ]
  }
}
```

**Excel Export Response:**

```json
{
  "isSuccess": true,
  "data": {
    "fileUrl": "/storage/reports/daily-sales-report-2025-01-01-to-2025-01-31.xlsx",
    "message": "Report exported successfully"
  }
}
```

**PDF Export Response:**

```json
{
  "isSuccess": true,
  "data": {
    "fileUrl": "/storage/reports/daily-sales-report-2025-01-01-to-2025-01-31.pdf",
    "message": "Report exported successfully"
  }
}
```
