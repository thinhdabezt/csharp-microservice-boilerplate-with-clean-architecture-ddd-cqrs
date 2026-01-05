# Microservice Boilerplate - Clean Architecture + DDD + CQRS

A production-ready boilerplate for building microservices using ASP.NET Core 8.0 with Clean Architecture, Domain-Driven Design (DDD), and Command Query Responsibility Segregation (CQRS) patterns.

## Architecture Overview

This boilerplate follows Clean Architecture principles with clear separation of concerns across four main layers:

### Domain Layer
- **Entities**: Business objects with identity (e.g., Product, ProductReview)
- **Value Objects**: Immutable objects with no identity (e.g., Money, Address)
- **Aggregates**: Clusters of domain objects treated as a single unit
- **Domain Events**: Events that occur within the domain
- **Repository Interfaces**: Abstractions for data access

### Application Layer
- **Commands**: Write operations (Create, Update, Delete)
- **Queries**: Read operations (Get, List, Search)
- **Handlers**: Process commands and queries
- **DTOs**: Data transfer objects
- **Validators**: FluentValidation for input validation
- **Behaviors**: MediatR pipeline behaviors (Logging, Validation, Performance)

### Infrastructure Layer
- **DbContext**: Entity Framework Core configuration
- **Repositories**: Implementation of repository interfaces
- **Configurations**: Entity configurations for EF Core
- **External Services**: Third-party service integrations

### API Layer
- **Controllers**: RESTful API endpoints
- **Middleware**: Exception handling, request logging
- **Configuration**: Swagger, CORS, Health checks

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Database**: SQL Server (configurable)
- **Validation**: FluentValidation
- **Logging**: Serilog
- **API Documentation**: Swagger/OpenAPI
- **Mediator**: MediatR
- **Mapping**: AutoMapper

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server (or Docker for SQL Server)
- Visual Studio 2022 / VS Code / Rider

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd BoilerPlate
   ```

2. **Update Connection String**
   
   Edit `src/API/appsettings.json` and update the connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Your connection string here"
   }
   ```

3. **Run Migrations**
   ```bash
   cd src/Infrastructure
   dotnet ef migrations add InitialCreate --startup-project ../API
   dotnet ef database update --startup-project ../API
   ```

4. **Run the Application**
   ```bash
   cd src/API
   dotnet run
   ```

5. **Access Swagger UI**
   
   Navigate to: `https://localhost:5001/swagger`

## Docker Support

### Build and Run with Docker Compose

```bash
docker-compose up --build
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001

## Project Structure

```
BoilerPlate/
├── src/
│   ├── Domain/                  # Core business logic and domain models
│   │   ├── Common/             # Base classes and interfaces
│   │   ├── Entities/           # Domain entities
│   │   ├── Events/             # Domain events
│   │   ├── Repositories/       # Repository interfaces
│   │   └── ValueObjects/       # Value objects
│   │
│   ├── Application/            # Business rules and use cases
│   │   ├── Common/            # Shared application logic
│   │   │   ├── Behaviors/     # MediatR behaviors
│   │   │   ├── Interfaces/    # Application interfaces
│   │   │   └── Models/        # Common models (Result, PagedResult)
│   │   └── Products/          # Product feature
│   │       ├── Commands/      # Write operations
│   │       └── Queries/       # Read operations
│   │
│   ├── Infrastructure/         # External dependencies
│   │   ├── Persistence/       # Database context and configurations
│   │   │   ├── Configurations/
│   │   │   └── Repositories/
│   │   └── Services/          # External service implementations
│   │
│   └── API/                    # Presentation layer
│       ├── Controllers/       # API controllers
│       ├── Middleware/        # Custom middleware
│       └── Program.cs         # Application entry point
│
├── Dockerfile
├── docker-compose.yml
└── MicroserviceBoilerplate.sln
```

## Design Patterns Used

### Clean Architecture
- Dependency Rule: Dependencies point inward
- Domain layer has no external dependencies
- Application layer depends only on Domain
- Infrastructure and API depend on Application

### Domain-Driven Design (DDD)
- **Aggregates**: Product aggregate with ProductReview entities
- **Value Objects**: Money, Address
- **Domain Events**: ProductCreated, ProductUpdated, ProductStockChanged
- **Repository Pattern**: Abstract data access
- **Unit of Work**: Transaction management

### CQRS (Command Query Responsibility Segregation)
- **Commands**: Modify state (CreateProduct, UpdateProduct)
- **Queries**: Read state (GetProduct, GetProducts)
- **Handlers**: Separate handlers for each command/query
- **MediatR**: Implements mediator pattern for CQRS

### Additional Patterns
- **Result Pattern**: Type-safe error handling
- **Pipeline Behavior**: Cross-cutting concerns (validation, logging, performance)
- **Repository Pattern**: Data access abstraction
- **Dependency Injection**: IoC container for loose coupling

## Key Features

- ✅ Clean Architecture with clear layer separation
- ✅ Domain-Driven Design with aggregates and value objects
- ✅ CQRS pattern with MediatR
- ✅ FluentValidation for input validation
- ✅ Global exception handling middleware
- ✅ Request/Response logging
- ✅ Structured logging with Serilog
- ✅ Swagger documentation
- ✅ Health checks
- ✅ Docker support
- ✅ EF Core with repository pattern
- ✅ Domain events
- ✅ Unit of Work pattern
- ✅ Result pattern for error handling

## API Endpoints

### Products

- `GET /api/products` - Get all products (paginated)
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product

### Health

- `GET /health` - Health check endpoint

## Adding New Features

### 1. Create Domain Entities

Add your entities to `src/Domain/Entities/`

### 2. Create Repository Interface

Add repository interface to `src/Domain/Repositories/`

### 3. Create Commands/Queries

Add commands to `src/Application/YourFeature/Commands/`
Add queries to `src/Application/YourFeature/Queries/`

### 4. Create Handlers

Implement handlers for your commands and queries

### 5. Create Validators

Add FluentValidation validators for your commands

### 6. Implement Repository

Implement repository in `src/Infrastructure/Persistence/Repositories/`

### 7. Create Controller

Add API controller in `src/API/Controllers/`

### 8. Configure EF Core

Add entity configuration in `src/Infrastructure/Persistence/Configurations/`

## Best Practices

1. **Keep Domain Pure**: Domain layer should have no dependencies on external frameworks
2. **Single Responsibility**: Each command/query should do one thing
3. **Validate Early**: Use FluentValidation in command validators
4. **Use Domain Events**: Communicate between aggregates using domain events
5. **Handle Errors Gracefully**: Use Result pattern for expected errors
6. **Log Appropriately**: Use structured logging for better observability
7. **Test Your Code**: Write unit tests for business logic

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.

## Support

For questions and support, please open an issue in the GitHub repository.
