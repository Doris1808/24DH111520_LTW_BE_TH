# 24DH111520_LTW_BE_TH

ASP.NET Core MVC E-Commerce Website with PostgreSQL

## Database Setup

### 1. Install PostgreSQL
brew install postgresql
brew services start postgresql

### 2. Create Database

createdb -U jasonluong mystore
### 3. Import Database
psql -U jasonluong -d mystore -f database-full.sql

### 4. Update Connection String

Create `appsettings.json`:
{
"ConnectionStrings": {
"MyStoreContext": "Host=localhost;Port=5432;Database=mystore;Username=jasonluong;Password=YOUR_PASSWORD"
}
}

### 5. Run Application
dotnet restore
dotnet run
## Technologies
- ASP.NET Core MVC 8.0
- PostgreSQL 16
- Entity Framework Core + Npgsql
- Bootstrap 5

