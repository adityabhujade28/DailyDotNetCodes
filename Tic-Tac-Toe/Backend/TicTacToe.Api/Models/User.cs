using System;

namespace TicTacToe.Api.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int TotalWins { get; set; } = 0;
    }
}