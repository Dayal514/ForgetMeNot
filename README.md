# 🌸 ForgetMeNot

ForgetMeNot is a lightweight **ASP.NET Core 8 Web API** for managing tasks and reminders.  
It’s built as a portfolio project to demonstrate **modern .NET development**, **cloud deployment**, and **CI/CD workflows**.

---

## 🚀 Features
- **Task Management (CRUD)**
  - Create, read, update, and delete tasks
  - Each task includes: `Title`, `Notes`, `DueDateUtc`, `IsDone`, `CreatedUtc`, `CompletedUtc`
- **Authentication & Authorization**
  - JWT-based auth with user registration and login
  - Token validation integrated with Swagger UI
- **Entity Framework Core** with automatic migrations at startup
- **Swagger / OpenAPI** enabled for live API exploration
- Deployed to **Azure App Service** with **Azure SQL Database**
- CI/CD via **GitHub Actions** (auto-deploys on push to `main`)

---

## 🛠️ Tech Stack
- **Backend:** ASP.NET Core 8 Web API (Controllers)
- **Database:** Entity Framework Core + SQL Server → Azure SQL (cloud)
- **Auth:** JSON Web Token (JWT) authentication
- **Cloud:** Azure App Service + Azure SQL Database
- **CI/CD:** GitHub Actions → Azure Web App deploy
- **Logging:** App Service Log Stream for runtime diagnostics
- **Testing:** xUnit (planned for unit/integration tests)

---

## 🌐 Live Demo
The API is deployed to Azure:  
👉 [Swagger UI](https://forgetmenot-api-g2bkhhcdedbsgwfe.westus3-01.azurewebsites.net/swagger/index.html)

> ⚠️ Note: Swagger is open for demo purposes. In a production system, access would typically be restricted.

---

## 📦 Getting Started (Local Dev)

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code
- (Optional) Azure subscription if you want to replicate cloud deployment

### Setup
1. Clone the repo:
   ```bash
   git clone https://github.com/<your-username>/ForgetMeNot.git
   cd ForgetMeNot
2. Update appsettings.Development.json with your local SQL Server connection string.
3. Apply migrations and run:
   ```bash
   dotnet ef database update
   dotnet run
4. Open Swagger at:   
   ```bash
   https://localhost:5001/swagger/index.html

---

## 📌 Roadmap
- [ ] Add unit and integration tests  
- [ ] Frontend client (React or Angular) consuming the API  
- [ ] Expand API with recurring reminders & categories  
- [ ] Move JWT secrets into Azure Key Vault for production hardening

---

## 📖 Why This Project?
This project was built to:
- Practice **modernizing skills** (from .NET Framework → .NET 8)  
- Learn **Azure App Service + Azure SQL** deployments  
- Implement a real-world **CI/CD pipeline** with GitHub Actions  
- Serve as a **portfolio project** to showcase full-stack .NET + cloud abilities
