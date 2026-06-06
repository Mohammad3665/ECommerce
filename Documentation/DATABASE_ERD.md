## Entity Relationship Diagram (ERD)

### Relationships Summary

| Entity   | Relationship    | Entity                         |
| -------- | --------------- | ------------------------------ |
| User     | (1) ----< (n)   | Order                          |
| User     | (1) ----- (1)   | Cart                           |
| User     | (n) ----- (n)   | Role                           |
| Order    | (1) ----- (1)   | OrderShipping                  |
| Order    | (1) ----- (1)   | OrderPayment                   |
| Order    | (1) ----- (1)   | Invoice                   |
| Order    | (1) ----< (n)   | OrderHistory                   |
| Order    | (n) ----- (0-1) | Coupon                         |
| Order    | (1) ----< (n)   | OrderItem                      |
| Product  | (n) >---- (1)   | Brand                          |
| Product  | (1) ----< (n)   | OrderItem                      |
| Product  | (n) >---- (1)   | Brand                          |
| Product  | (n) >---- (1)   | Category                       |
| Product  | (1) ----< (n)   | ProductImage                   |
| Product  | (1) ----< (n)   | ProductSpecification           |
| Category | (n) >---- (1)   | SubCategories(Self-Reference)  |
| Category | (n) >---- (1)   | ParentCategory(Self-Reference) |
| Cart     | (1) ----< (n)   | CartItem                       |
| CartItem | (n) >---- (1)   | Product                        |
| Comment  | (n) >---- (1)   | CartItem                       |
| Comment  | (n) ----- (0-1) | ParentComment                  |
| Article  | (n) >---- (1)   | ArticleCategory                |
| Article  | (n) >---- (1)   | User (Author)                  |
