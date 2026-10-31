# Employee Leave Management System

A RESTful Employee Leave Management API built with ASP.NET Core Web API, Entity Framework Core, and PostgreSQL.

This project provides backend functionality for managing employees and their leave requests using a layered architecture.

## Overview

The Employee Leave Management System is a backend application designed to manage employee information and leave application workflows.

It allows users to create and manage employee records, submit leave requests, retrieve leave information, and update leave request statuses.

## Features

### Employee Management

* Create new employees.
* Retrieve all employees.
* Retrieve employee details by ID.
* Update employee information.
* Delete employees.
* Validate employee email addresses.
* Prevent duplicate employee emails.

### Leave Request Management

* Create leave requests.
* Retrieve all leave requests.
* Retrieve leave requests by ID.
* Retrieve leave requests by employee ID.
* Update leave request status.
* Delete leave requests.
* Validate leave dates.
* Validate allowed leave types.
* Verify employee existence before creating leave requests.

### Leave Request Status

Supported statuses:

* Pending
* Approved
* Rejected

New leave requests are automatically assigned the `Pending` status.

## Technologies Used

| Technology              | Purpose                 |
| ----------------------- | ----------------------- |
| C#                      | Programming Language    |
| .NET 10                 | Backend Framework       |
| ASP.NET Core Web API    | REST API Development    |
| Entity Framework Core   | ORM                     |
| PostgreSQL 17           | Relational Database     |
| Npgsql                  | PostgreSQL Provider     |
| Visual Studio Community | Development Environment |
| Postman                 | API Testing             |
| Git & GitHub            | Version Control         |

## Architecture

The project follows a layered architecture inspired by Clean Architecture principles.

```text
                  API Layer
                     |
                     v
              Application Layer
                     |
                     v
                Domain Layer

             Infrastructure Layer
                     |
                     v
               PostgreSQL
```

### Project Structure

```text
LeaveManagementApi/
│
├── LeaveManagementApi/
│   ├── Controllers/
│   ├── Properties/
│   ├── Program.cs
│   └── appsettings.json
│
├── LeaveManagementApi.Application/
│   ├── DTOs/
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   └── Services/
│   ├── Services/
│   └── DependencyInjection/
│
├── LeaveManagementApi.Domain/
│   └── Entities/
│
├── LeaveManagementApi.Infrastructure/
│   ├── Persistence/
│   │   └── Migrations/
│   ├── Repositories/
│   └── DependencyInjection/
│
├── .gitignore
├── README.md
└── LeaveManagementApi.slnx
```

## Database Design

The application currently contains two main entities.

### Employee

| Field       | Description                       |
| ----------- | --------------------------------- |
| Id          | Unique Employee Identifier (UUID) |
| FullName    | Employee Name                     |
| Email       | Employee Email                    |
| Department  | Department Name                   |
| Designation | Job Designation                   |
| JoiningDate | Employee Joining Date             |
| IsActive    | Employee Active Status            |

### LeaveRequest

| Field      | Description                            |
| ---------- | -------------------------------------- |
| Id         | Unique Leave Request Identifier (UUID) |
| EmployeeId | Associated Employee ID                 |
| LeaveType  | Type of Leave                          |
| StartDate  | Leave Start Date                       |
| EndDate    | Leave End Date                         |
| Reason     | Leave Reason                           |
| Status     | Request Status                         |
| AppliedAt  | Application Submission Date            |

### Relationship

One Employee can have multiple Leave Requests.

```text
Employee (1)
     |
     |
     | One-to-Many
     |
     v
LeaveRequest (Many)
```

## API Endpoints

Base URL:

```text
https://localhost:7299
```

### Employee Endpoints

| Method | Endpoint              | Description        |
| ------ | --------------------- | ------------------ |
| GET    | `/api/Employees`      | Get all employees  |
| GET    | `/api/Employees/{id}` | Get employee by ID |
| POST   | `/api/Employees`      | Create employee    |
| PUT    | `/api/Employees/{id}` | Update employee    |
| DELETE | `/api/Employees/{id}` | Delete employee    |

### Leave Request Endpoints

| Method | Endpoint                                   | Description              |
| ------ | ------------------------------------------ | ------------------------ |
| GET    | `/api/LeaveRequests`                       | Get all leave requests   |
| GET    | `/api/LeaveRequests/{id}`                  | Get leave request by ID  |
| GET    | `/api/LeaveRequests/employee/{employeeId}` | Get requests by employee |
| POST   | `/api/LeaveRequests`                       | Create leave request     |
| PATCH  | `/api/LeaveRequests/{id}/status`           | Update request status    |
| DELETE | `/api/LeaveRequests/{id}`                  | Delete leave request     |

## Installation and Setup

### Prerequisites

Make sure you have installed:

* .NET 10 SDK
* PostgreSQL 17
* Visual Studio Community
* Git

### 1. Clone the Repository

```bash
git clone https://github.com/mahmudul99sohan/LeaveManagementApi.git
```

### 2. Navigate to the Project

```bash
cd LeaveManagementApi
```

### 3. Configure Database Connection

Create a PostgreSQL database named:

```text
leave_management_db
```

Configure the connection string locally. Do not commit your actual database password.

You can use .NET User Secrets:

```bash
dotnet user-secrets init --project LeaveManagementApi/LeaveManagementApi.csproj
```

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=leave_management_db;Username=postgres;Password=YOUR_LOCAL_PASSWORD" --project LeaveManagementApi/LeaveManagementApi.csproj
```

Replace `YOUR_LOCAL_PASSWORD` with your local PostgreSQL password.

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Apply Database Migrations

```bash
dotnet ef database update --project LeaveManagementApi.Infrastructure/LeaveManagementApi.Infrastructure.csproj --startup-project LeaveManagementApi/LeaveManagementApi.csproj
```

### 6. Run the Application

```bash
dotnet run --project LeaveManagementApi/LeaveManagementApi.csproj
```

The API will start using the configured application launch settings.

## API Testing

The endpoints have been tested using Postman.

The tested operations include:

* Employee CRUD operations.
* Leave Request CRUD operations.
* Leave status updates.
* Invalid leave date validation.
* Invalid leave type validation.
* Duplicate employee email validation.

## Future Improvements

The following features are planned for future development:

* JWT Authentication and Authorization.
* Role-Based Access Control.
* Improved global exception handling.
* Structured logging.
* Unit and Integration Testing.
* Improved API documentation.
* Pagination and filtering.
* Enhanced database validation.

## Developer

**Mahmudul Hasan**

Computer Science and Engineering Graduate

GitHub: [mahmudul99sohan](https://github.com/mahmudul99sohan)

---

If you find this project useful, feel free to explore the repository.
