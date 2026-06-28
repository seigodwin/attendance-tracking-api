# Attendance Tracking API

[![.NET CI](https://github.com/seigodwin/attendance-tracking-api/actions/workflows/main.yml/badge.svg)](https://github.com/seigodwin/attendance-tracking-api/actions/workflows/main.yml)

## 📌 Overview

The **Attendance Tracking API** is a robust backend system built with .NET 10 for managing and tracking employee attendance records. It provides a secure, scalable, and well-architected foundation for recording employee check-ins, check-outs, and maintaining comprehensive attendance history.

This API is designed to be consumed by a React frontend (coming soon) and will be deployed to Render for production use.

---

## 🚀 Features

- **Employee Management**: Create, update, and manage employee profiles
- **Admin Management**: Administrative user creation and role management
- **Attendance Tracking**: Real-time check-in / check-out functionality
- **Attendance History**: Comprehensive attendance records and reporting
- **User Authentication**: Secure JWT-based authentication with token expiration
- **Role-Based Access Control (RBAC)**: Admin and Employee role differentiation
- **Caching Layer**: Redis-based distributed caching for improved performance
- **API Documentation**: Interactive API documentation via Scalar
- **Database Migrations**: Version-controlled schema management with Entity Framework Core
- **Environment Configuration**: Secure configuration management using `.env` files

---

## 🛠️ Tech Stack

- **.NET 10** - Modern ASP.NET Core Web API framework
- **Entity Framework Core 10** - ORM with PostgreSQL provider
- **PostgreSQL** - Relational database
- **JWT (JSON Web Tokens)** - Stateless authentication
- **Redis** - Distributed caching
- **Scalar** - Interactive API documentation
- **DotNetEnv** - Environment variable management
- **EFCore.NamingConventions** - CamelCase naming convention for database columns
- **ASP.NET Core Identity** - User and role management

---

## 🧱 Project Architecture

### Directory Structure

```text
src/AttendanceTrackingApi/
├── Controllers/
│   ├── AdminController.cs           - Admin user management endpoints
│   ├── AttendanceController.cs       - Attendance check-in/check-out endpoints
│   ├── AuthController.cs            - Authentication endpoints
│   └── EmployeeController.cs        - Employee management endpoints
├── Services/
│   ├── Application/
│   │   ├── Interfaces/              - Application service contracts
│   │   └── Implimentations/         - Business logic implementations
│   ├── Auth/
│   │   ├── Interface/               - Auth service contracts
│   │   └── Implimentations/         - Authentication implementations
│   ├── Repository/
│   │   ├── Interfaces/              - Repository contracts
│   │   └── Implimentations/         - Data access implementations
│   └── Caching/
│       ├── Interfaces/              - Caching contracts
│       └── Implimentations/         - Redis caching implementations
├── Domain/
│   ├── Entities/
│   │   ├── Admin.cs                 - Admin entity with ASP.NET Identity
│   │   ├── Employee.cs              - Employee entity
│   │   └── Attendance.cs            - Attendance records entity
│   └── Dtos/
│       ├── AdminDtos/               - Admin data transfer objects
│       ├── EmployeeDtos/            - Employee data transfer objects
│       └── AuthDtos/                - Authentication DTOs
├── DbContext/
│   └── AppDbContext.cs              - Entity Framework DbContext configuration
├── Migrations/                       - Database schema versions
├── Options/
│   └── JwtOptions.cs                - JWT configuration options
└── Program.cs                        - Application startup configuration
```

### Layered Architecture

```text
Controllers (API Layer)
        ↓
Services (Business Logic)
        ↓
Repositories (Data Access)
        ↓
Database (PostgreSQL) + Cache (Redis)
```

---

## ⚙️ Setup Instructions

### Prerequisites

- **.NET 10 SDK** or later
- **PostgreSQL** (version 12+)
- **Redis** (for caching)
- **Git**

### 1. Clone the repository

```bash
git clone https://github.com/seigodwin/attendance-tracking-api.git
cd attendance-tracking-api
```

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Configure Environment Variables

Create a `.env` file in the root of `src/AttendanceTrackingApi/` directory:

```env
# Database Configuration
DB_CONNECTION_STRING=Host=localhost;Port=5432;Database=attendance_tracking_db;Username=postgres;Password=your_password

# JWT Configuration
JWT_SECRET=your_super_secret_key_here_min_32_characters
JWT_ISSUER=AttendanceTrackingAPI
JWT_AUDIENCE=AttendanceTrackingAPIUsers
JWT_EXPIRATION=3600

# Redis Configuration
REDIS_HOST=localhost:6379
```

**Important**: Never commit the `.env` file with sensitive information. Add it to `.gitignore`.

### 4. Database Setup

#### Create the Database

```bash
# Ensure PostgreSQL is running
createdb attendance_tracking_db
```

#### Run Migrations

```bash
cd src/AttendanceTrackingApi
dotnet ef database update
```

To create a new migration:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### 5. Run the Application

```bash
cd src/AttendanceTrackingApi
dotnet run
```

The API will be available at:

- **API Base URL**: `https://localhost:5001` (HTTPS) or `http://localhost:5000` (HTTP)
- **Swagger/Scalar Documentation**: `https://localhost:5001/scalar/v1`

---

## 🔑 Environment Variables

| Variable | Description | Example |
| --- | --- | --- |
| `JWT_SECRET` | Secret key for signing JWT tokens (min 32 chars) | `your_super_secret_key_min_32_characters_long` |
| `JWT_ISSUER` | Token issuer identifier | `AttendanceTrackingAPI` |
| `JWT_AUDIENCE` | Token audience identifier | `AttendanceTrackingAPIUsers` |
| `JWT_EXPIRATION` | Token expiration time in seconds | `3600` |
| `REDIS_HOST` | Redis server connection string | `localhost:6379` |
| `DB_CONNECTION_STRING` | PostgreSQL connection string | `Host=localhost;Port=5432;Database=attendance_tracking_db;Username=postgres;Password=password` |

---

## 📡 API Endpoints

### Authentication Endpoints (`/api/auth`)

| Method | Endpoint | Description | Auth Required |
| --- | --- | --- | --- |
| `POST` | `/api/auth/register-admin` | Register a new admin user | ❌ |
| `POST` | `/api/auth/login` | Login and receive JWT token | ❌ |
| `POST` | `/api/auth/refresh-token` | Refresh expired JWT token | ✅ |

### Employee Endpoints (`/api/employee`)

| Method | Endpoint | Description | Auth Required | Role |
| --- | --- | --- | --- | --- |
| `GET` | `/api/employee` | Get all employees | ✅ | Admin |
| `GET` | `/api/employee/{id}` | Get employee by ID | ✅ | Admin/Employee |
| `POST` | `/api/employee` | Create new employee | ✅ | Admin |
| `PUT` | `/api/employee/{id}` | Update employee | ✅ | Admin |
| `DELETE` | `/api/employee/{id}` | Delete employee | ✅ | Admin |

### Attendance Endpoints (`/api/attendance`)

| Method | Endpoint | Description | Auth Required | Role |
| --- | --- | --- | --- | --- |
| `GET` | `/api/attendance` | Get all attendance records | ✅ | Admin |
| `GET` | `/api/attendance/{id}` | Get attendance by ID | ✅ | Admin/Employee |
| `POST` | `/api/attendance/check-in` | Check in employee | ✅ | Employee |
| `POST` | `/api/attendance/check-out` | Check out employee | ✅ | Employee |
| `GET` | `/api/attendance/history/{employeeId}` | Get employee attendance history | ✅ | Admin/Employee |

### Admin Endpoints (`/api/admin`)

| Method | Endpoint | Description | Auth Required | Role |
| --- | --- | --- | --- | --- |
| `GET` | `/api/admin` | Get all admins | ✅ | Admin |
| `GET` | `/api/admin/{id}` | Get admin by ID | ✅ | Admin |
| `POST` | `/api/admin` | Create new admin | ✅ | Admin |
| `PUT` | `/api/admin/{id}` | Update admin | ✅ | Admin |
| `DELETE` | `/api/admin/{id}` | Delete admin | ✅ | Admin |

---

## 🔐 Authentication

### JWT Token Structure

Tokens are issued with claims containing user roles and identifiers. Include the token in the `Authorization` header:

```http
Authorization: Bearer <your_jwt_token>
```

### Example Login Flow

1. **Register Admin**:

   ```bash
   POST /api/auth/register-admin
   Content-Type: application/json
   
   {
     "email": "admin@example.com",
     "password": "SecurePassword123!"
   }
   ```

2. **Login**:

   ```bash
   POST /api/auth/login
   Content-Type: application/json
   
   {
     "email": "admin@example.com",
     "password": "SecurePassword123!"
   }
   ```

3. **Use Token** in subsequent requests:

   ```bash
   GET /api/employee
   Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
   ```

---

## 🗄️ Database Schema

### Key Entities

#### Admin

- `Id` (GUID) - Primary key
- `Email` - Unique email address
- `PasswordHash` - Hashed password
- `CreatedAt` - Creation timestamp
- `UpdatedAt` - Last update timestamp

#### Employee

- `Id` (GUID) - Primary key
- `FirstName` - Employee first name
- `LastName` - Employee last name
- `Email` - Unique email address
- `Department` - Department/team
- `EmploymentDate` - Hire date
- `CreatedAt` - Creation timestamp
- `UpdatedAt` - Last update timestamp

#### Attendance

- `Id` (GUID) - Primary key
- `EmployeeId` (GUID) - Foreign key to Employee
- `Date` (Date) - Attendance date
- `CheckInTime` (TimeOnly) - Check-in timestamp
- `CheckOutTime` (TimeOnly) - Check-out timestamp (nullable)
- `Duration` - Time spent at work
- `CreatedAt` - Record creation timestamp

---

## 💾 Caching Strategy

The API uses **Redis** for distributed caching to improve performance:

- **Employee Data**: Cached for frequently accessed employee records
- **Attendance Records**: Cached recent attendance history
- **Cache Expiration**: Configurable TTL (Time-To-Live) for cache entries

Redis is optional but recommended for production environments.

---

## 🚀 Deployment

### Deploying to Render

1. Connect your GitHub repository to Render
2. Set the build command:

   ```bash
   dotnet publish -c Release -o out
   ```

3. Set the start command:

   ```bash
   dotnet out/AttendanceTrackingApi.dll
   ```

4. Add environment variables in Render dashboard
5. Deploy!

---

## 📝 Testing

To run tests (when available):

```bash
dotnet test
```

---

## 🐛 Common Issues & Troubleshooting

### PostgreSQL Connection Failed

- Ensure PostgreSQL is running on `localhost:5432`
- Verify credentials in `.env` file
- Check database exists: `psql -U postgres -l | grep attendance_tracking_db`

### Redis Connection Failed

- Ensure Redis is running on `localhost:6379`
- For Windows, download Redis from: [https://github.com/microsoftarchive/redis/releases](https://github.com/microsoftarchive/redis/releases)

### JWT Token Expired

- Get a new token by calling the `/api/auth/refresh-token` endpoint with valid credentials

### Migrations Not Found

- Ensure you're in the `src/AttendanceTrackingApi` directory
- Run: `dotnet ef migrations add InitialCreate`

---

## 📚 API Documentation

Once the application is running, access interactive API documentation at:

```bash
https://localhost:5001/scalar/v1
```

This provides a user-friendly interface to explore and test all available endpoints.

---

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature`
3. Commit changes: `git commit -m "Add your feature"`
4. Push to branch: `git push origin feature/your-feature`
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## 📞 Support

For issues, questions, or suggestions, please open an issue on the GitHub repository.

---

Happy tracking! 🎯
