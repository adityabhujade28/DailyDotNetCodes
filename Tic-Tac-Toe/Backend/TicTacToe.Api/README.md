TicTacToe.Api

Endpoints:
- POST /api/auth/register { username, password }
- POST /api/auth/login { username, password } -> token (user id placeholder)
- POST /api/game/create { playerXId, playerOId } -> { id }
- POST /api/game/{id}/move { playerId, row, col } -> { isFinished, winner }

DB: sqlite stored at Data Source=tictactoe.db
