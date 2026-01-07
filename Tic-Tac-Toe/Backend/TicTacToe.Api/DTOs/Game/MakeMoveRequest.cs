using System;

namespace TicTacToe.Api.DTOs.Game
{
    public record MakeMoveRequest(Guid PlayerId, int Row, int Col);
}