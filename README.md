# Demo Projects - Background Services & Hangfire in .NET Core

## Overview

This repository contains demo projects to illustrate different ways of running background tasks in ASP.NET Core:

1. **Background Services Demo**  
   Demonstrates using `BackgroundService`, `IHostedService`, timers, and scoped services to run recurring background tasks safely.

2. **Hangfire Demo**  
   Demonstrates how to use Hangfire to run and manage background jobs, including fire-and-forget, delayed, and recurring jobs.

---

## Projects

### 1. Background Services Demo

**Description:**  
A collection of example projects showing various patterns for background tasks, including continuous loops, scheduled execution, and safe use of scoped services inside singleton hosted services.

**Key Concepts:**  
- `BackgroundService` and `IHostedService`  
- Timers for scheduled tasks  
- Dependency Injection: Singleton vs Scoped  
- CancellationToken for graceful shutdown  
- Logging background tasks  

**Usage:**  
1. Open the project in Visual Studio or VS Code.  
2. Run the project.  
3. Observe logs in the console showing recurring background task execution.

---

### 2. Hangfire Demo

**Description:**  
A simple demo project using Hangfire to run background jobs in ASP.NET Core. Includes examples of recurring, delayed, and fire-and-forget jobs.

**Key Concepts:**  
- Hangfire setup and configuration  
- Recurring, delayed, and fire-and-forget jobs  
- Integration with scoped services like DbContext  
- Monitoring jobs via Hangfire Dashboard  

**Usage:**  
1. Open the project in Visual Studio or VS Code.  
2. Run the project.  
3. Access Hangfire Dashboard (usually `/hangfire`) to monitor jobs.  
4. Observe job execution logs in console or database.

---

## How to Use

1. Clone the repository:  
   ```bash
   git clone <repo-url>
