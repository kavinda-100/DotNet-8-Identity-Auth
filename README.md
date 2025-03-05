# DotNet-8-Identity-Auth

## Asp.Net Core 8 Role-Based Authorization and Claims with JWT Bearer Token Authentication

This project will cover `Role-Based Authorization and Claims` with `JWT Bearer Token` Authentication.
and use `Extension Method` for `ServiceCollection` and `ApplicationBuilder` for better code readability.

### Tools and Technologies
- JetBrains Rider
- Dot-Net 8
- Asp.Net Core 8 web API
- JWT Bearer Token Package
- Identity Package
- PostgresSql Database

### Installed Nuget Packages
```bash
// for db
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

// for prevent object cycle
dotnet add package Newtonsoft.json
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

// for authentication with identity pakage
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
```

### Project Structure
- **Models**: Data Models (Tables)
- **Data**: Database Context and Models
- **Controllers**: API Endpoints
- **DTOs**: Data Transfer Objects
- **Extensions**: Extension Methods
- **Migrations**: Database Migrations
