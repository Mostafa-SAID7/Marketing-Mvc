# Market MVC

A modern ASP.NET Core MVC e-commerce platform with clean architecture, featuring product management, order processing, and customer management.

## 🚀 Features

- **Product Management** - Create, read, update, and delete products with categories
- **Order Management** - Process and track customer orders with payment status
- **Customer Management** - Manage customer profiles and address information
- **Authentication & Authorization** - Built-in user identity management
- **Dashboard & Analytics** - Real-time statistics and business metrics
- **Responsive Design** - Mobile-first UI with Bootstrap 5
- **Database Migrations** - Entity Framework Core with SQL Server support

## 🏗️ Architecture

This project follows **Clean Architecture** principles with separation of concerns:

```
market-mvc/
├── Controllers/       # MVC Controllers
├── Models/           # Domain Models & ViewModels
├── Services/         # Business Logic Layer
├── Repositoriers/    # Data Access Layer
├── Data/             # DbContext & Database Configuration
├── Features/         # CQRS Commands & Queries
├── Infrastructure/   # Cross-cutting concerns
├── Views/            # Razor Views
├── wwwroot/          # Static files (CSS, JS, images)
├── Configs/          # Configuration classes
├── Extensions/       # Extension methods
└── Migrations/       # EF Core Migrations
```

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: SQL Server
- **ORM**: Entity Framework Core 9.0.10
- **Architecture**: Clean Architecture + CQRS Pattern
- **Validation**: FluentValidation 11.9.0
- **Mapping**: AutoMapper 12.0.1
- **Mediator**: MediatR 14.0.0
- **Identity**: ASP.NET Core Identity

## 📋 Prerequisites

- .NET 9.0 SDK or later
- SQL Server 2019 or later
- Visual Studio 2022 or Visual Studio Code
- Git

## 🔧 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/Mostafa-SAID7/Marketing-Mvc.git
cd Marketing-Mvc
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. Configure Database Connection
Update `appsettings.json` with your SQL Server connection string:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=market_mvc_db;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=true;"
  }
}
```

### 4. Apply Migrations
```bash
dotnet ef database update
```

### 5. Run the Application
```bash
dotnet run --project market-mvc
```

The application will be available at `https://localhost:5001`

## 📚 Database Schema

### Core Entities

- **Products** - E-commerce products with pricing and inventory
- **Categories** - Product categorization
- **Orders** - Customer orders with payment tracking
- **OrderItems** - Line items in orders
- **Customers** - Customer profiles with addresses
- **Items** - Generic item catalog

### Key Features
- Soft delete support (IsDeleted flag)
- Audit trails (CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeletedAt, DeletedBy)
- Value Objects for Address and PersonName
- Order and Payment status tracking

## 🔐 Authentication

The application uses ASP.NET Core Identity for user authentication and authorization.

### Default Admin Account
- **Username**: admin@market-mvc.com
- **Password**: AdminPassword123! (Change on first login)

## 📖 Project Structure

### Models
- `Models/entity/` - Core domain entities
- `Models/ViewModels/` - View models for each feature
- `Models/ObjectValues/` - Value objects (Address, PersonName, Money)
- `Models/Enums/` - Enumerations (ProductStatus, OrderStatus, etc.)

### Services & Repositories
- `Services/` - Business logic and service implementations
- `Repositoriers/` - Data access patterns and queries
- `Infrastructure/` - Base classes and specifications

### CQRS Implementation
- `Features/` - Organized by feature with Commands, Queries, and Validators

## 🧪 Testing

To run tests (when test projects are added):
```bash
dotnet test
```

## 🤝 Contributing

1. Create a feature branch (`git checkout -b feature/amazing-feature`)
2. Commit your changes (`git commit -m 'Add amazing feature'`)
3. Push to the branch (`git push origin feature/amazing-feature`)
4. Open a Pull Request

## 📝 Commit Guidelines

- Use clear, descriptive commit messages
- Reference issues when applicable (#issue-number)
- Follow conventional commits format: `type(scope): description`

Examples:
- `feat(products): add product filtering`
- `fix(orders): resolve payment status update`
- `docs(readme): update installation steps`
- `refactor(services): simplify order processing`

## 🐛 Known Issues

- None currently documented

## 📞 Support

For support, open an issue on GitHub or contact the development team.

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👥 Authors

- **Mostafa SAID** - Initial work and architecture

## 🗺️ Roadmap

- [ ] Admin Dashboard Implementation
- [ ] Payment Gateway Integration
- [ ] Email Notifications
- [ ] Product Reviews & Ratings
- [ ] Shopping Cart Persistence
- [ ] Advanced Search & Filtering
- [ ] API Documentation (Swagger)
- [ ] Unit & Integration Tests

## 📞 Contact

For questions or suggestions, please reach out through GitHub Issues.

---

**Last Updated**: June 2026
