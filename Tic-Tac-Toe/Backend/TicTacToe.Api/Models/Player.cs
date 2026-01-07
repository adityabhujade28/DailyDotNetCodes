using System;
using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Models
{
    public class Player
    {
        public Guid UserId { get; set; }
        public PlayerSymbol Symbol { get; set; }
    }
}