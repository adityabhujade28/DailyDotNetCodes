using Microsoft.EntityFrameworkCore;
using TicTacToe.Api.Data;
using TicTacToe.Api.Models;
using System;
using System.Threading.Tasks;

namespace TicTacToe.Api.Stores
{
    public class EfGameStore : IGameStore
    {
        private readonly AppDbContext _db;
        public EfGameStore(AppDbContext db) { _db = db; }

        public async Task CreateAsync(Game game)
        {
            _db.Games.Add(game);
            _db.SetBoardJson(game);
            await _db.SaveChangesAsync();
        }

        public async Task<Game?> GetAsync(Guid id)
        {
            var g = await _db.Games.FindAsync(id);
            if (g == null) return null;
            _db.LoadBoardFromJson(g);
            return g;
        }

        public async Task UpdateAsync(Game game)
        {
            _db.SetBoardJson(game);
            _db.Games.Update(game);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetByUserAsync(Guid userId)
        {
            var list = await _db.Games.Where(g => g.PlayerXId == userId || g.PlayerOId == userId).ToListAsync();
            foreach(var g in list) _db.LoadBoardFromJson(g);
            return list;
        }

        public async Task DeleteUnfinishedAsync(Guid userId)
        {
            var unfinished = await _db.Games
                .Where(g => (g.PlayerXId == userId || g.PlayerOId == userId) && !g.IsFinished)
                .ToListAsync();
            _db.Games.RemoveRange(unfinished);
            await _db.SaveChangesAsync();
        }
    }
}