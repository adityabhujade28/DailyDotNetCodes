using System;

namespace TicTacToe.Api.Game.Models
{
    public record Move(int Row, int Col, Guid PlayerId);
}