# 🌸 ForgetMeNot

ForgetMeNot is a lightweight **ASP.NET Core 8 Web API** for managing tasks and reminders.  
It’s designed as a portfolio project to demonstrate modern .NET skills, cloud deployment, and CI/CD workflows.

---

## 🚀 Features
- Create, read, update, and delete tasks (CRUD API)
- Each task includes:
  - `Title`
  - `Notes`
  - `DueDate`
  - `IsDone`
- Built with **ASP.NET Core 8** and **Entity Framework Core**
- **Swagger / OpenAPI** enabled for quick testing
- Ready for deployment to **Azure App Service** with **Azure SQL Database**

---

## 🛠️ Tech Stack
- **Backend:** ASP.NET Core 8 Web API (Controllers)
- **Database:** Entity Framework Core + SQL Server (local) → Azure SQL (cloud)
- **Auth:** (Planned) JSON Web Token (JWT) authentication
- **CI/CD:** (Planned) GitHub Actions → Azure App Service
- **Testing:** xUnit for unit tests

---

## 📦 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Setup
1. Clone the repo:
   ```bash
   git clone https://github.com/<your-username>/ForgetMeNot.git
   cd ForgetMeNot
