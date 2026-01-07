using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Auth.Models;

namespace TicTacToe.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}