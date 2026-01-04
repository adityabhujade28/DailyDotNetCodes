# SchoolManagement.Api

Quick setup notes to run this project on another PC:

Prerequisites
- Install .NET 8 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Install or enable a SQL Server instance (Developer/Express) or ensure LocalDB is available if using Visual Studio.

Recommended steps
1. Open a terminal in `SchoolManagement.Api` folder.
2. Restore packages: `dotnet restore`
3. Build: `dotnet build`
4. Run: `dotnet run`

Database
- The default connection string in `appsettings.json` is:
  `Server=.;Database=SchoolDb;Trusted_Connection=True;TrustServerCertificate=True`
  This points to a local SQL Server default instance. If you don't have SQL Server installed, you can either:
  - Install SQL Server (Express or Developer), or
  - Change the connection string to LocalDB: `Server=(localdb)\\MSSQLLocalDB;Database=SchoolDb;Trusted_Connection=True;`
  - Or run SQL Server in Docker and update the connection string accordingly.

Applying Migrations
- Migrations are included in the project. The app now applies pending migrations automatically on startup.
- Alternatively you can apply migrations manually:
  1. Install the EF tool if not present: `dotnet tool install --global dotnet-ef`
  2. Run: `dotnet ef database update`

Troubleshooting
- If you get a runtime error about missing SDK, run `dotnet --info` to confirm the installed SDKs.
- If the app fails to connect to the DB, verify the connection string and that SQL Server is running.

If you'd like, I can change the default connection to LocalDB or add a Docker compose for SQL Server — tell me which you prefer.