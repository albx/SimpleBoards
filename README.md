# SimpleBoards
SimpleBoards is a demo project used to try the .NET 5 features.

It's a simple project management tool where you can add boards and issues (like a small GitHub Project).

## Requirements
In order to run this project you need the last **.NET 5** SDK and a **SQL Server Express** instance.

## Projects
SimpleBoards consists of some projects:

- **SimpleBoards.Core**: it's the project where are defined the domain classes and services. It uses Entity Framework Core 5 as persistence ORM.
- **SimpleBoards.Persistence.SqlServer**: it's and utility project which contains the Entity Framework Core 5 Migrations for SQL Server.
- **SimpleBoards.Identity.Core**: it's a class library project which contains models and DbContext for the ASP.NET Identity part.
- **SimpleBoards.Identity**: it's an IdentityServer4 project which manages all the Authentication/Authorization features.
- **SimpleBoards.Identity.Grpc**: it's a gRPC project which exposes the endpoint to synchronize users between the WebApi project and the Identity project (like a simple OpenHost implementation)
- **SimpleBoards.Web.Api**: it's the ASP.NET Core 5 project which exposes all the REST endpoints.
- **SimpleBoards.Web.App**: it's the Blazor WebAssembly client app
- **SimpleBoards.Web.Models**: it's a shared class library which contains the presentation models used both by the Web Api and the Blazor client.
