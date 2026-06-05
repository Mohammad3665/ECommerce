# API Specification - ECommerce

## Base URL

Development: https://localhost:5162/api/v1

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

| Method | Endpoint                 | Description               | Access       |
| ------ | ------------------------ | ------------------------- | ------------ |
| GET    | /Admin/Users             | Get all users (paginated) | Admin, Owner |
| GET    | /Admin/Users/{id}        | Get user by id            | Admin, Owner |
| POST   | /Admin/Users             | Create user               | Admin, Owner |
| PUT    | /Admin/Users/{id}        | Update user               | Admin, Owner |
| DELETE | /Admin/Users/{id}        | Delete user               | Owner only   |
| PATCH  | /Admin/Users/{id}/Status | Activate/deactivate user  | Admin, Owner |
| PATCH  | /Admin/Users/{id}/Role   | Change user role          | Owner only   |

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

| Method | Endpoint          | Description       | Access    |
| ------ | ----------------- | ----------------- | --------- |
| GET    | /Profile          | Get my profile    | Customer+ |
| PUT    | /Profile          | Update my profile | Customer+ |
| GET    | /Profile/Orders   | Get my orders     | Customer+ |
| GET    | /Profile/Comments | Get my comments   | Customer+ |

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

| Method | Endpoint                  | Description               | Access       |
| ------ | ------------------------- | ------------------------- | ------------ |
| GET    | /Brands                   | Get all brands            | Public       |
| GET    | /Brands/{id}              | Get brand by id           | Public       |
| GET    | /Brands/{slug}            | Get brand by slug         | Public       |
| GET    | /Brands/{id}/Products     | Get products by brand     | Public       |
| POST   | /Admin/Brands             | Create brand              | Admin, Owner |
| PUT    | /Admin/Brands/{id}        | Update brand              | Admin, Owner |
| DELETE | /Admin/Brands/{id}        | Delete brand              | Admin, Owner |
| PATCH  | /Admin/Brands/{id}/Status | Activate/deactivate brand | Admin, Owner |

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

| Method | Endpoint                      | Description                  | Access       |
| ------ | ----------------------------- | ---------------------------- | ------------ |
| GET    | /Categories                   | Get all categories           | Public       |
| GET    | /Categories/{id}              | Get category by id           | Public       |
| GET    | /Categories/{slug}            | Get category by slug         | Public       |
| GET    | /Categories/{id}/Products     | Get products by category     | Public       |
| POST   | /Admin/Categories             | Create category              | Admin, Owner |
| PUT    | /Admin/Categories/{id}        | Update category              | Admin, Owner |
| DELETE | /Admin/Categories/{id}        | Delete category              | Admin, Owner |
| PATCH  | /Admin/Categories/{id}/Status | Activate/deactivate category | Admin, Owner |

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

## 6. Products

| Method | Endpoint                                              | Description                 | Access       |
| ------ | ----------------------------------------------------- | --------------------------- | ------------ |
| GET    | /Products                                             | Get products with filters   | Public       |
| GET    | /Products/{id}                                        | Get product by id           | Public       |
| GET    | /Products/{slug}                                      | Get product by slug         | Public       |
| GET    | /Products/{id}/Comments                               | Get product comments        | Public       |
| POST   | /Admin/Products                                       | Create product              | Admin, Owner |
| PUT    | /Admin/Products/{id}                                  | Update product              | Admin, Owner |
| DELETE | /Admin/Products/{id}                                  | Delete product              | Admin, Owner |
| PATCH  | /Admin/Products/{id}/Status                           | Activate/deactivate product | Admin, Owner |
| POST   | /Admin/Products/{id}/Images                           | Upload product images       | Admin, Owner |
| DELETE | /Admin/Products/{productId}/Images/{imageId}          | Delete product image        | Admin, Owner |
| PATCH  | /Admin/Products/{productId}/Images/{imageId}/Set-main | Set main image              | Admin, Owner |
| POST   | /Admin/Products/{id}/Specifications                   | Add specifications          | Admin, Owner |
| PUT    | /Admin/Products/{productId}/Specifications/{specId}   | Update specification        | Admin, Owner |
| DELETE | /Admin/Products/{productId}/Specifications/{specId}   | Delete specification        | Admin, Owner |

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

| Method | Endpoint                 | Description             | Access    |
| ------ | ------------------------ | ----------------------- | --------- |
| GET    | /Cart                    | Get my cart             | Customer+ |
| POST   | /Cart/Items              | Add item to cart        | Customer+ |
| PUT    | /Cart/Items/{cartItemId} | Update item quantity    | Customer+ |
| DELETE | /Cart/Items/{cartItemId} | Remove item from cart   | Customer+ |
| DELETE | /Cart/Clear              | Clear cart              | Customer+ |
| POST   | /Cart/Apply-coupon       | Apply coupon to cart    | Customer+ |
| DELETE | /Cart/Remove-coupon      | Remove coupon from cart | Customer+ |

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

| Method | Endpoint                  | Description               | Access       |
| ------ | ------------------------- | ------------------------- | ------------ |
| POST   | /Orders                   | Place new order           | Customer+    |
| GET    | /Orders                   | Get my orders             | Customer+    |
| GET    | /Orders/{id}              | Get order details         | Customer+    |
| GET    | /Admin/Orders             | Get all orders            | Admin, Owner |
| GET    | /Admin/Orders/{id}        | Get order details (admin) | Admin, Owner |
| PUT    | /Admin/Orders/{id}/Status | Update order status       | Admin, Owner |
| PUT    | /Admin/Orders/{id}/Cancel | Cancel order              | Admin, Owner |

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

| Method | Endpoint                  | Description               | Access    |
| ------ | ------------------------- | ------------------------- | --------- |
| POST   | /Payment/Verify           | Verify payment (callback) | Public    |
| GET    | /Payment/Status/{orderId} | Check payment status      | Customer+ |

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

| Method | Endpoint            | Description      | Access       |
| ------ | ------------------- | ---------------- | ------------ |
| GET    | /Admin/Coupons      | Get all coupons  | Admin, Owner |
| GET    | /Admin/Coupons/{id} | Get coupon by id | Admin, Owner |
| POST   | /Admin/Coupons      | Create coupon    | Admin, Owner |
| PUT    | /Admin/Coupons/{id} | Update coupon    | Admin, Owner |
| DELETE | /Admin/Coupons/{id} | Delete coupon    | Admin, Owner |

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

| Method | Endpoint                       | Description            | Access          |
| ------ | ------------------------------ | ---------------------- | --------------- |
| POST   | /Products/{productId}/Comments | Add comment to product | Customer+       |
| PUT    | /Comments/{id}                 | Edit my comment        | Customer+       |
| DELETE | /Comments/{id}                 | Delete my comment      | Customer+       |
| GET    | /Admin/Comments/Pending        | Get pending comments   | ContentManager+ |
| GET    | /Admin/Comments/Approved       | Get approved comments  | ContentManager+ |
| GET    | /Admin/Comments/Rejected       | Get rejected comments  | ContentManager+ |
| PUT    | /Admin/Comments/{id}/Approve   | Approve comment        | ContentManager+ |
| PUT    | /Admin/Comments/{id}/Reject    | Reject comment         | ContentManager+ |

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

| Method | Endpoint                           | Description                   | Access          |
| ------ | ---------------------------------- | ----------------------------- | --------------- |
| GET    | /Articles                          | Get all articles              | Public          |
| GET    | /Articles/{id}                     | Get article by id             | Public          |
| GET    | /Articles/{slug}                   | Get article by slug           | Public          |
| GET    | /Articles/Latest                   | Get latest articles (5 items) | Public          |
| GET    | /Articles/Categories               | Get article categories        | Public          |
| GET    | /Articles/Categories/{id}/Articles | Get articles by category      | Public          |
| POST   | /Admin/Articles                    | Create article                | ContentManager+ |
| PUT    | /Admin/Articles/{id}               | Update article                | ContentManager+ |
| DELETE | /Admin/Articles/{id}               | Delete article                | ContentManager+ |
| POST   | /Admin/Articles/Categories         | Create article category       | ContentManager+ |
| PUT    | /Admin/Articles/Categories/{id}    | Update article category       | ContentManager+ |
| DELETE | /Admin/Articles/Categories/{id}    | Delete article category       | ContentManager+ |

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

| Method | Endpoint                   | Description                | Access       |
| ------ | -------------------------- | -------------------------- | ------------ |
| GET    | /Sliders                   | Get all active sliders     | Public       |
| GET    | /Admin/Sliders             | Get all sliders (admin)    | Admin, Owner |
| GET    | /Admin/Sliders/{id}        | Get slider by id           | Admin, Owner |
| POST   | /Admin/Sliders             | Create slider              | Admin, Owner |
| PUT    | /Admin/Sliders/{id}        | Update slider              | Admin, Owner |
| DELETE | /Admin/Sliders/{id}        | Delete slider              | Admin, Owner |
| PATCH  | /Admin/Sliders/{id}/Status | Activate/deactivate slider | Admin, Owner |

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
| GET | /Admin/Dashboard/Statistics | Get overall statistics | Admin, Owner |
| GET | /Admin/Dashboard/Sales | Get sales report | Admin, Owner |
| GET | /Admin/Dashboard/Top-Products | Get most viewed products | Admin, Owner |
| GET | /Admin/Dashboard/Orders-Status | Get orders by status | Admin, Owner |

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

| Method | Endpoint      | Description           | Access   |
| ------ | ------------- | --------------------- | -------- |
| GET    | /Health/Ready | Readiness probe       | Public   |
| GET    | /Health/Live  | Liveness probe        | Public   |
| GET    | /Metrics      | OpenTelemetry metrics | Internal |

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

| Entity   | Example URL                         |
| -------- | ----------------------------------- |
| Product  | /api/v1/Products/iphone-15-pro      |
| Category | /api/v1/Categories/smartphones      |
| Brand    | /api/v1/Brands/apple                |
| Article  | /api/v1/Articles/top-10-smartphones |

## 16. Role Management (Owner only)

**Base Path:** `/api/v1/Owner/Roles`

| Method | Endpoint                                       | Description                 | Access     |
| ------ | ---------------------------------------------- | --------------------------- | ---------- |
| GET    | `/Owner/Roles`                                 | Get all roles               | Owner only |
| GET    | `/Owner/Roles/{id}`                            | Get role by id              | Owner only |
| POST   | `/Owner/Roles`                                 | Create new role             | Owner only |
| PUT    | `/Owner/Roles/{id}`                            | Update role                 | Owner only |
| DELETE | `/Owner/Roles/{id}`                            | Delete role                 | Owner only |
| GET    | `/Owner/Roles/{id}/Permissions`                | Get role permissions        | Owner only |
| POST   | `/Owner/Roles/{id}/Permissions`                | Assign permissions to role  | Owner only |
| DELETE | `/Owner/Roles/{id}/Permissions/{permissionId}` | Remove permission from role | Owner only |
| GET    | `/Owner/Roles/{id}/Users`                      | Get users with this role    | Owner only |

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
      "name": "Owner",
      "displayName": "مالک",
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

## 17. Permission Management (Owner Only)

**Base Path:** `/api/v1/Owner/Permissions`

| Method | Endpoint                   | Description                             | Access     |
| ------ | -------------------------- | --------------------------------------- | ---------- |
| GET    | /Owner/Permissions         | Get all permissions (grouped by module) | Owner only |
| GET    | /Owner/Permissions/Modules | Get all permission modules              | Owner only |

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

## 18. Assign Roles to Users (Admin/Owner)

**Base Path:** `/api/v1/Admin/Users`

| Method | Endpoint                             | Description           | Access       |
| ------ | ------------------------------------ | --------------------- | ------------ |
| POST   | /Admin/Users/{userId}/Roles          | Assign roles to user  | Admin, Owner |
| GET    | /Admin/Users/{userId}/Roles          | Get user roles        | Admin, Owner |
| DELETE | /Admin/Users/{userId}/Roles/{roleId} | Remove role from user | Admin, Owner |

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
