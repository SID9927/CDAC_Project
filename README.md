# Farmers Market Place

## Project Overview

The Farmers Market Place is a comprehensive e-commerce platform designed to connect farmers directly with consumers. This project aims to streamline the agricultural supply chain by providing a user-friendly interface for buying and selling farm-fresh produce and other agricultural goods.

## Repository

The main repository for this project is located at:
https://github.com/SID9927/CDAC_PROJECT.git

## Project Structure

The repository contains multiple components of the Farmers Market Place project:

1. **Spring Boot Backend**: Java-based backend application
2. **ASP.NET Core Backend**: C#-based backend alternative
3. **React Frontend**: Responsive web interface for users

All these components are saved in separate folders within the main repository.

## Features

- User Authentication (Admin, Farmer, Customer)
- Product Management
- Shopping Cart Functionality
- Order Processing
- Payment Integration
- User Profile Management
- Admin Dashboard
- Responsive Design
- API endpoints for farmers, products, stock details, and user interactions
- CORS support for multiple origins
- Swagger API documentation
- Global exception handling

## Tech Stack

- **Frontend**: 
  - React.js
  - Bootstrap
  - React Router
  - Axios (for HTTP requests)

- **Backend**: 
  - Java (Spring Boot)
  - ASP.NET Core (C#)
  - MySQL Database
  - Entity Framework Core (for .NET)
  - Spring Data JPA (for Java)

- **Tools & Libraries**:
  - Maven (Java dependency management)
  - Newtonsoft.Json (.NET)
  - AutoMapper (.NET)
  - Swagger (API documentation)

## Getting Started

1. Clone the repository:

git clone https://github.com/SID9927/CDAC_PROJECT.git

2. Navigate to the project directory:

cd CDAC_PROJECT


### Spring Boot Backend

1. Ensure you have Java JDK 8 or higher installed
2. Navigate to the Spring Boot project directory
3. Run: `./mvnw spring-boot:run`
4. Access the application at `http://localhost:9090`

### ASP.NET Core Backend

1. Ensure you have .NET Core SDK installed
2. Navigate to the ASP.NET Core project directory
3. Run `dotnet restore` to install dependencies
4. Update connection strings in `appsettings.json`
5. Run `dotnet run` to start the application

### React Frontend

1. Navigate to the React project directory
2. Run `npm install` to install dependencies
3. Run `npm start` to start the development server

## API Documentation

For the ASP.NET Core backend, Swagger UI is available at the `/swagger` endpoint when running in development mode.

## Contributing

We welcome contributions to improve the Farmers Market Place. Please feel free to submit issues and pull requests to the main repository.

## License

This project is licensed under the MIT License.

## Acknowledgements

- React Bootstrap
- React Credit Cards
- React Hot Toast
- React Icons
