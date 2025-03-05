# DotNet-8-Identity-Auth

## Asp.Net Core 8 Role-Based Authorization and Claims with JWT Bearer Token Authentication

This project will cover `Role-Based Authorization and Claims` with `JWT Bearer Token` Authentication.

### Tools and Technologies
- JetBrains Rider
- Dot-Net 8
- Asp.Net Core 8 web API
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