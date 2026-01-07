using System;
using System.Threading.Tasks;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Stores
{
    public interface IGameStore
    {
        Task CreateAsync(Game game);
        Task<Game?> GetAsync(Guid id);
        Task UpdateAsync(Game game);
        Task<IEnumerable<Game>> GetByUserAsync(Guid userId);
        Task DeleteUnfinishedAsync(Guid userId);
    }
}