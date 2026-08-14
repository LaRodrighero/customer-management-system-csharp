# Customer Management System in C#

Console-based customer management system developed in C# to practice object-oriented programming, CRUD operations, data validation, persistence, and code organization.

## Features

- Customer registration
- Customer listing
- Search by name or CPF
- Customer update
- Customer removal
- CPF validation
- Birth date validation
- Sorting by name or birth date
- JSON data persistence
- Simple user authentication
- Repository pattern with interfaces

## Technologies

- C#
- .NET
- LINQ
- JSON
- Object-Oriented Programming
- Repository Pattern

## Project Structure

- `Cliente.cs` - Customer model
- `Usuarios.cs` - User model
- `IRepositorioBase.cs` - Generic repository interface
- `IRepositorioCli.cs` - Customer repository interface
- `RepositorioCli.cs` - In-memory customer repository
- `RepositorioCliJson.cs` - JSON customer persistence
- `RepositorioUsuarioJson.cs` - JSON user persistence
- `Validador.cs` - Data validation
- `OrdenadorClientes.cs` - Customer sorting service
- `RepOrdenarCli.cs` - Sorting logic

## Learning Goals

This project was created as part of my learning journey in C# and software development, focusing on:

- Object-oriented programming
- Separation of responsibilities
- Interfaces and abstraction
- Data persistence
- Input validation
- LINQ and collections
- Code organization and maintainability

## Future Improvements

- Migrate the application to ASP.NET Core Web API
- Replace JSON persistence with a relational database
- Implement authentication with JWT
- Add automated tests
- Add Docker support
