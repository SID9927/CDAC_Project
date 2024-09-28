# FarmerMarketPlaceASP.NetCore

FarmerMarketPlaceASP.NetCore is a robust ASP.NET Core backend application for a farmer's market platform. It provides a set of APIs to manage farmers, products, stock details, and user interactions.

## Features

- CORS support for multiple origins
- MySQL database integration
- Swagger API documentation
- Global exception handling
- Dependency Injection for services and repositories
- Entity Framework Core for database operations
- Newtonsoft.Json for handling JSON serialization
- AutoMapper for object mapping

## Getting Started

### Prerequisites

- .NET Core SDK
- MySQL Server

### Installation

1. Clone the repository
2. Navigate to the project directory
3. Run `dotnet restore` to install dependencies
4. Update the connection strings in `appsettings.json` for both Development and Production environments
5. Run `dotnet run` to start the application

## API Documentation

When running in development mode, Swagger UI is available at `/swagger` endpoint for interactive API documentation.

## Configuration

The application uses different connection strings for Development and Production environments. Make sure to set up your `appsettings.json` file accordingly.

## Deployment

The application is configured to work with reverse proxies and supports forwarded headers.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
