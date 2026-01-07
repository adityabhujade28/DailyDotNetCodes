using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Stores
{
    public class InMemoryGameStore
    {
        private readonly ConcurrentDictionary<Guid, Game> _games = new();

        public Task CreateAsync(Game g)
        {
            _games[g.Id] = g; return Task.CompletedTask;
        }

        public Task<Game?> GetAsync(Guid id) => Task.FromResult(_games.TryGetValue(id, out var g) ? g : null);

        public Task UpdateAsync(Game g)
        {
            _games[g.Id] = g; return Task.CompletedTask;
        }

        public Task<IEnumerable<Game>> GetByUserAsync(Guid userId)
        {
            var list = _games.Values.Where(g => g.PlayerXId == userId || g.PlayerOId == userId).ToList();
            return Task.FromResult<IEnumerable<Game>>(list);
        }
    }
}