# Customer Management System in C#

Console-based customer management system developed in C# with focus on object-oriented programming, CRUD operations, data validation, JSON persistence, authentication, and code organization.

## Features

* Customer registration
* Customer listing
* Search by name or CPF
* Customer update
* Customer removal
* CPF validation
* Birth date validation
* Sorting by name or birth date
* JSON data persistence
* Simple user authentication
* Repository pattern with interfaces

## Technologies

* C#
* .NET 9
* LINQ
* JSON
* Object-Oriented Programming
* Repository Pattern

## Project Structure

### App
- `Program.cs` - Application entry point and main system flow

### Models
- `Cliente.cs` - Customer model
- `Usuarios.cs` - User model

### Repositories
- `IRepositorioBase.cs` - Generic repository interface
- `IRepositorioCli.cs` - Customer repository interface
- `RepositorioCli.cs` - In-memory customer repository
- `RepositorioCliJson.cs` - JSON-based customer persistence
- `RepositorioUsuarioJson.cs` - JSON-based user persistence

### Services
- `OrdenadorClientes.cs` - Customer sorting service
- `RepOrdenarCli.cs` - Sorting logic using LINQ

### Utils
- `Validador.cs` - CPF and birth date validation

### Main Files
- `clientes.json` - Customer data persistence
- `usuarios.json` - User data persistence
- `CrudCompleto.csproj` - .NET project configuration
- `desafios.cs` - Development challenges and learning notes

## Concepts Applied

This project applies software development concepts such as:

* Object-oriented programming
* Separation of responsibilities
* Interfaces and abstraction
* Repository pattern
* Data persistence
* Input validation
* LINQ and collections
* Basic authentication flow
* Code organization and maintainability

## How to Run

Requirements:

* .NET 9 SDK

Clone the repository:

```bash
git clone https://github.com/LaRodrighero/customer-management-system-csharp.git
```

Navigate to the project folder:

```bash
cd customer-management-system-csharp
```

Run the application:

```bash
dotnet run
```

## Future Improvements

* Migrate the application to ASP.NET Core Web API
* Replace JSON persistence with a relational database
* Implement authentication with JWT
* Add automated tests
* Add Docker support
