# ProductFinder API

## Project Overview

**ProductFinder API** is a RESTful API designed for managing products. It provides endpoints for CRUD (Create, Read, Update, Delete) operations on product data and includes authentication and authorization features using JWT (JSON Web Tokens).

## Technologies Used

- **.NET 8.0**: Framework for building the application.
- **ASP.NET Core**: Web framework used for building the API.
- **Serilog**: Logging library for .NET applications.
- **FluentValidation**: Library for fluent validation of models.
- **Entity Framework Core**: ORM for database access.
- **SQL Server**: Database for storing product data.

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio or any other IDE that supports .NET development

### Installation

1. **Clone the Repository:**

   ```bash
   git clone <repository-url>
   cd ProductFinder

2. **Restore NuGet Packages:**

```bash
dotnet restore


3. **Update Connection String:**

- **Ensure the connection string in ProductDbContext is correctly set up for your SQL Server instance.

Apply Migrations:

Apply the initial database migration to create the database schema.

bash
Kodu kopyala
dotnet ef migrations add InitialCreate
dotnet ef database update
Run the Application:

bash
Kodu kopyala
dotnet run
The API will be available at https://localhost:7168/.
