Backend/TicTacToe.Api

This is a minimal, in-memory TicTacToe API.

Endpoints:
- POST /api/auth/register { username, password }
- POST /api/auth/login { username, password } -> token (user id string)
- POST /api/game/create { playerXId, playerOId } -> { id }
- POST /api/game/{id}/move { playerId, row, col } -> { isFinished, winner }
- GET  /api/game/{id} -> game object

To run:
- dotnet build
- dotnet run --project Backend/TicTacToe.Api

Notes: Replace token generation with JWT for production.