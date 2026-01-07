using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicTacToe.Api.Models;
using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(builder =>
            {
                // Don't map the complex Board object directly; store cells as JSON in shadow property
                builder.Ignore(g => g.Board);
                builder.Property<string>("BoardJson");
            });
        }

        public static PlayerSymbol[][] ToJagged(PlayerSymbol[,] cells)
        {
            if (cells == null) return new PlayerSymbol[0][];
            int rows = cells.GetLength(0);
            int cols = cells.GetLength(1);
            var jagged = new PlayerSymbol[rows][];
            for (int r = 0; r < rows; r++)
            {
                jagged[r] = new PlayerSymbol[cols];
                for (int c = 0; c < cols; c++) jagged[r][c] = cells[r, c];
            }
            return jagged;
        }

        public void SetBoardJson(Game game)
        {
            var jagged = ToJagged(game.Board.Cells);
            var json = JsonSerializer.Serialize(jagged);
            Entry(game).Property("BoardJson").CurrentValue = json;
        }

        public void LoadBoardFromJson(Game game)
        {
            var json = Entry(game).Property<string>("BoardJson").CurrentValue as string;
            if (!string.IsNullOrEmpty(json))
            {
                var jagged = JsonSerializer.Deserialize<PlayerSymbol[][]>(json);
                if (jagged != null)
                {
                    int rows = jagged.Length;
                    int cols = jagged.Length > 0 ? jagged[0].Length : 0;
                    var arr = new PlayerSymbol[rows, cols];
                    for (int r = 0; r < rows; r++) for (int c = 0; c < cols; c++) arr[r, c] = jagged[r][c];
                    game.Board.Cells = arr;
                }
            }
        }
    }
}