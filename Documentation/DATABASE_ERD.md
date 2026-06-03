## Entity Relationship Diagram (ERD)

### Relationships Summary

| Entity   | Relationship    | Entity               |
| -------- | --------------- | -------------------- |
| User     | (1) ----< (n)   | Order                |
| Product  | (n) >---- (1)   | Brand                |
| User     | (1) ----- (1)   | Cart                 |
| Cart     | (1) ----< (n)   | CartItem             |
| CartItem | (n) >---- (1)   | Product              |
| Product  | (1) ----< (n)   | OrderItem            |
| Order    | (1) ----< (n)   | OrderItem            |
| Product  | (n) >---- (1)   | Brand                |
| Product  | (n) >---- (1)   | Category             |
| Product  | (1) ----< (n)   | ProductImage         |
| Product  | (1) ----< (n)   | ProductSpecification |
| Order    | (1) ----< (n)   | OrderHistory         |
| Order    | (n) ----- (0-1) | Coupon               |
| Comment  | (n) >---- (1)   | CartItem             |
| Comment  | (n) ----- (0-1) | ParentComment        |
| Article  | (n) >---- (1)   | ArticleCategory      |
| Article  | (n) >---- (1)   | User (Author)        |
