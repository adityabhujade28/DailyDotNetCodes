Using SQL Server (LocalDB) for migrations & DB creation

1) Ensure EF CLI is available:
   dotnet tool install --global dotnet-ef

2) Add packages (already added in csproj):
   Microsoft.EntityFrameworkCore
   Microsoft.EntityFrameworkCore.SqlServer
   Microsoft.EntityFrameworkCore.Design

3) Create migration (run from project folder):
   cd Backend/TicTacToe.Api
   dotnet ef migrations add InitialCreate -o Data/Migrations

4) Create / update database:
   dotnet ef database update

5) If you want a different SQL Server, replace `DefaultConnection` in `appsettings.json` with your connection string.

Notes:
- The Game board is stored as JSON in the `BoardJson` column for simplicity.
- Replace LocalDB connection with a production SQL Server as needed.