# Technical Specification - ECommerce Project

## Project Structure (Clean Architecture)

```markdown
ECommerce/
├── src/
│ ├── ECommerce.Domain/ # Entities, Value Objects, Enums
│ │ ├── Entities/
│ │ ├── Enums/
│ │ └── Interfaces/
│ │
│ ├── ECommerce.Application/ # DTOs, Commands, Queries, Handlers
│ │ ├── Common/
│ │ │ ├── Behaviors/ # Validation, Logging, Caching
│ │ │ ├── Exceptions/
│ │ │ └── Mappings/
│ │ ├── Features/
│ │ │ ├── Products/
│ │ │ │ ├── Commands/
│ │ │ │ ├── Queries/
│ │ │ │ └── Validators/
│ │ │ ├── Orders/
│ │ │ ├── Users/
│ │ │ └── ...
│ │ └── Contracts/ # Request/Response models
│ │
│ ├── ECommerce.Infrastructure/ # Data Access, Caching, External Services
│ │ ├── Persistence/
│ │ │ ├── Context/
│ │ │ ├── Repositories/
│ │ │ ├── Configurations/
│ │ │ └── Migrations/
│ │ ├── Services/
│ │ │ ├── CacheService.cs
│ │ │ ├── PaymentGatewaySimulator.cs
│ │ │ └── ShippingService.cs
│ │ └── Identity/
│ │ ├── JwtService.cs
│ │ └── PasswordHasher.cs
│ │
│ └── ECommerce.API/ # Controllers, Middlewares, Program.cs
│ ├── Controllers/
│ ├── Middlewares/
│ ├── Filters/
│ └── appsettings.json
│
├── tests/ # (Optional - not required for now)
├── docker-compose.yml
├── Dockerfile
└── README.md
```
