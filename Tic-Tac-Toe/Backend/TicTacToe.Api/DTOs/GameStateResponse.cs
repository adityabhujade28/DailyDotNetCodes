using TicTacToe.Api.Game.Models;
using System;

namespace TicTacToe.Api.DTOs
{
    public record GameStateResponse(Guid Id, PlayerSymbol[,] Board, PlayerSymbol Turn, bool IsFinished, PlayerSymbol Winner);
}