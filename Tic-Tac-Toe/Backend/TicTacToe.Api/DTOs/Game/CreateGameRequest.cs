using System;

namespace TicTacToe.Api.DTOs.Game
{
    public record CreateGameRequest(Guid PlayerXId, Guid PlayerOId);
}