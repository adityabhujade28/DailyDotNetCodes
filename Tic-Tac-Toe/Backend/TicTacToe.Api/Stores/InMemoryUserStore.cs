using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Stores
{
    public class InMemoryUserStore
    {
        private readonly ConcurrentDictionary<Guid, User> _users = new();

        public Task CreateAsync(User user)
        {
            _users[user.Id] = user; return Task.CompletedTask;
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            foreach(var u in _users.Values) if (u.Username == username) return Task.FromResult<User?>(u);
            return Task.FromResult<User?>(null);
        }

        public Task<User?> GetByIdAsync(Guid id) => Task.FromResult(_users.TryGetValue(id, out var u) ? u : null);

        public Task IncrementWinsAsync(Guid userId)
        {
            if (_users.TryGetValue(userId, out var u)) u.TotalWins++;
            return Task.CompletedTask;
        }
    }
}