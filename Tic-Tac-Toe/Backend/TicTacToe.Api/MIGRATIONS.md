Migration & Run instructions

1) Install EF Core CLI (if not installed):
   dotnet tool install --global dotnet-ef

2) From the Backend project folder:
   cd Backend/TicTacToe.Api

3) Add EF packages (if not already):
   dotnet add package Microsoft.EntityFrameworkCore.Sqlite
   dotnet add package Microsoft.EntityFrameworkCore.Design

4) Create migration and update DB:
   dotnet ef migrations add InitialCreate -o Data/Migrations
   dotnet ef database update

   Note: If you run from solution root use: 
   dotnet ef migrations add InitialCreate -p Backend/TicTacToe.Api -s Backend/TicTacToe.Api -o Backend/TicTacToe.Api/Data/Migrations

5) Run the API:
   dotnet run --project Backend/TicTacToe.Api

6) Test endpoints:
   POST /api/auth/register -> { username, password }
   POST /api/auth/login -> { username, password }
   POST /api/game/create -> { playerXId, playerOId }
   POST /api/game/{id}/move -> { playerId, row, col }

7) Frontend:
   Open Frontend/index.html in the browser (or serve with a static server like "npx serve Frontend").

Security note: Password hashing is implemented but tokens are placeholders (user id). Integrate real JWT tokens for production.