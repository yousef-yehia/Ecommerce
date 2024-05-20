# E-Commerce API
## Overview
This project is an ASP.NET Core API for an e-commerce platform. It provides functionalities for both administrators and customers to manage products, view product listings, and add products to a shopping basket. The project utilizes various design patterns and technologies to ensure scalability, security, and maintainability.

## Technologies Used
- ASP.NET Core: Framework for building cross-platform web applications.
- Entity Framework Core: Object-Relational Mapper (ORM) for database interactions.
- Identity Framework: Provides user authentication and authorization capabilities.
- SQL Server: Relational database management system used for data storage.
- Redis: Key-value database used for caching.
- JWT (JSON Web Tokens): Authentication mechanism for securing API endpoints.
- Swagger UI: Interactive API documentation tool.
- AutoMapper: Library for object-to-object mapping.
- Repository Pattern: Design pattern for abstracting data access.
- Specification Pattern: Design pattern for encapsulating query logic.
- Unit of Work Pattern: to manage transactions and ensure that multiple operations are treated as a single logical unit.


## Features
- Authentication and Authorization: JWT-based authentication ensures secure access to the API endpoints. Different roles are assigned to administrators and customers to control access to various functionalities.
- Product Management: Administrators can add, update, or delete products.
- Category Management: Administrators can perform CRUD operations on product categories.
- Product Listing: Customers can view the list of available products.
- Shopping Basket: Customers can add products to their shopping basket.
- Ordering Functionality: Customers will be able to place orders.
- Payment Integration (Planned): Integration with payment gateways for secure online payments.

## Installation
- Clone the repository to your local machine.
- Install the required dependencies using NuGet Package Manager.
- Configure the connection strings for SQL Server and Redis in the appsettings.json file.
- Run the database migrations to create the necessary tables.
- Build and run the application.

## Usage
- Administrator: As an administrator, you can:
1. Add, update, or delete products.
2. Perform CRUD operations on product categories.
- Customer: As a customer, you can:
1. View the list of available products.
2. Add products to your shopping basket.
3. Soon: Place orders and make payments.

## Roadmap
- Enhanced Security: Implement additional security measures such as HTTPS, input validation, and request throttling.
- Performance Optimization: Continuously optimize the application for improved speed and scalability.
