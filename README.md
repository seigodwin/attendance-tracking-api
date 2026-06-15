# Attendance Tracking API

[![.NET CI](https://github.com/seigodwin/attendance-tracking-api/actions/workflows/main.yml/badge.svg)](https://github.com/seigodwin/attendance-tracking-api/actions/workflows/main.yml)

## 📌 Overview

The **Attendance Tracking API** is a backend system built with .NET for managing and tracking employee attendance records. It provides a secure and scalable foundation for recording employee check-ins, check-outs, and attendance history.

This API is designed to be consumed by a React frontend (coming soon) and will be deployed to Render for production use.

---

## 🚀 Features

- Employee attendance tracking (check-in / check-out)
- Attendance history management
- User authentication (JWT-based)
- Role-based access control (Admin / Employee)
- Secure RESTful API design
- PostgreSQL database integration
- Redis caching support (optional)
- Environment variable configuration using `.env`

---

## 🛠️ Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Redis (caching)
- DotNetEnv
- xUnit (testing)
- GitHub Actions (CI)

---

## 🧱 Architecture

The system is structured into:

- Controllers → API endpoints
- Services → Business logic
- Repositories → Data access layer
- Database → PostgreSQL

---

## ⚙️ Setup Instructions

### 1. Clone the repository

```bash
git clone https://github.com/seigodwin/attendance-tracking-api.git
cd attendance-tracking-api
