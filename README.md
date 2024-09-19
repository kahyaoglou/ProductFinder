Absolutely! I can convert the information you provided into a well-formatted README file:

## ProductFinder API

### Project Overview

ProductFinder API is a RESTful API designed for managing product data. It offers functionalities for CRUD (Create, Read, Update, Delete) operations on products and incorporates authentication and authorization features utilizing JWT (JSON Web Tokens).

### Technologies Used

* **.NET 8.0:** Framework for building the application.
* **ASP.NET Core:** Web framework for constructing the API.
* **Serilog:** Logging library for .NET applications.
* **FluentValidation:** Library for model validation with a fluent syntax.
* **Entity Framework Core:** ORM (Object-Relational Mapping) for database access.
* **SQL Server:** Database for storing product data.

### Getting Started

#### Prerequisites

* **.NET 8.0 SDK**
* **SQL Server**
* **Visual Studio or any IDE** compatible with .NET development

#### Installation

1. **Clone the Repository:**

   ```bash
   git clone <repository-url>
   cd ProductFinder
   ```

2. **Restore NuGet Packages:**

   ```bash
   dotnet restore
   ```

3. **Update Connection String:**

   - Ensure the connection string in `ProductDbContext` is correctly configured for your SQL Server instance.

4. **Apply Migrations:**

   Run the following commands to create the initial database schema:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. **Run the Application:**

   ```bash
   dotnet run
   ```

   The API will be accessible at https://localhost:7168/.

### Configuration

Configuration settings are located in `appsettings.json`:

* **JWT Settings:**
    * Secret key: Used for signing JWT tokens.
    * Issuer: The issuer of the token.
    * Audience: The intended audience of the token.
* **Serilog Settings:**
    * Logs are written to `C:\Users\furka\Desktop\Logs\log.txt` with daily rotation.

### API Endpoints

#### Authentication

* **POST /api/auth/giris**

    Logs in a user and returns a JWT token. No authentication is required for this endpoint.

#### Products

* **GET /api/product**

    Retrieves a list of all products. Authentication is required.

* **GET /api/product/{id}**

    Retrieves a product by its ID. Authentication is required.

* **POST /api/product**

    Creates a new product. Authentication is required.

* **PUT /api/product**

    Updates an existing product. Authentication is required.

* **DELETE /api/product/{id}**

    Deletes a product by its ID. Authentication is required.

### Validation

FluentValidation is used to validate product creation and update requests. The `ProductValidator` class ensures:

* **Name:**
    * Not empty.
    * Length between 3 and 50 characters.
* **Category:**
    * Not empty.
    * Length between 3 and 50 characters.

### Logging

Serilog handles logging. Logs are written to a file and include details regarding requests, validation results, and exceptions.

### Contributing

We welcome contributions! Feel free to submit a pull request or open an issue for discussion.

### License

This project is licensed under the MIT License. Refer to the `LICENSE` file for details.

### Contact

For inquiries or support, please contact your-email@example.com.
