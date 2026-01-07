using TicTacToe.Api.Data;
using TicTacToe.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace TicTacToe.Api.Stores
{
    public class EfUserStore : IUserStore
    {
        private readonly AppDbContext _db;
        public EfUserStore(AppDbContext db) { _db = db; }

        public async Task CreateAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public Task<User?> GetByUsernameAsync(string username) =>
            _db.Users.FirstOrDefaultAsync(u => u.Username == username);

        public Task<User?> GetByIdAsync(Guid id) => _db.Users.FindAsync(id).AsTask().ContinueWith(t => (User?)t.Result);

        public async Task IncrementWinsAsync(Guid userId)
        {
            var u = await _db.Users.FindAsync(userId);
            if (u == null) return;
            u.TotalWins++;
            await _db.SaveChangesAsync();
        }
    }
}