using System;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Api.Auth.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public int TotalWins { get; set; } = 0;
    }
}