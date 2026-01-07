using System.Threading.Tasks;
using TicTacToe.Api.Services.Interfaces;
using TicTacToe.Api.Stores;
using TicTacToe.Api.DTOs.Game;
using System;
using TicTacToe.Api.Models;
using TicTacToe.Api.Enums;
using TicTacToe.Api.Data;

namespace TicTacToe.Api.Services
{
    public class GameService : IGameService
    {
        private readonly TicTacToe.Api.Stores.IGameStore _store;
        private readonly TicTacToe.Api.Stores.IUserStore _users;
        public GameService(TicTacToe.Api.Stores.IGameStore store, TicTacToe.Api.Stores.IUserStore users) { _store = store; _users = users; }

        public async Task<TicTacToe.Api.Models.Game> CreateGameAsync(Guid playerXId, Guid playerOId)
        {
            var g = new TicTacToe.Api.Models.Game { PlayerXId = playerXId, PlayerOId = playerOId };
            await _store.CreateAsync(g);
            return g;
        }

        public async Task<GameStateResponse?> MakeMoveAsync(Guid gameId, Guid playerId, int row, int col)
        {
            var g = await _store.GetAsync(gameId);
            if (g == null) return null;
            var ok = g.MakeMove(playerId, row, col);
            if (!ok) return null;
            if (g.IsFinished && g.Winner != PlayerSymbol.None)
            {
                var winnerId = g.Winner == PlayerSymbol.X ? g.PlayerXId : g.PlayerOId;
                await _users.IncrementWinsAsync(winnerId);
            }
            await _store.UpdateAsync(g);
            var jagged = AppDbContext.ToJagged(g.Board.Cells);
            return new GameStateResponse(g.Id, jagged, g.Turn, g.IsFinished, g.Winner);
        }

        public Task<TicTacToe.Api.Models.Game?> GetGameAsync(Guid id) => _store.GetAsync(id);

        public Task<IEnumerable<TicTacToe.Api.Models.Game>> GetGamesByUserAsync(Guid userId) => _store.GetByUserAsync(userId);

        public Task DeleteUnfinishedGamesAsync(Guid userId) => _store.DeleteUnfinishedAsync(userId);
    }
}