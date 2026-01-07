using System;
using TicTacToe.Api.Enums;

namespace TicTacToe.Api.DTOs.Game
{
    public record GameStateResponse(Guid Id, PlayerSymbol[][] Board, PlayerSymbol Turn, bool IsFinished, PlayerSymbol Winner);
}