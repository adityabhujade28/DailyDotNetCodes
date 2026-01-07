using System;
using System.Threading.Tasks;
using TicTacToe.Api.DTOs.Game;

namespace TicTacToe.Api.Services.Interfaces
{
    public interface IGameService
    {
        Task<TicTacToe.Api.Models.Game> CreateGameAsync(Guid playerXId, Guid playerOId);
        Task<GameStateResponse?> MakeMoveAsync(Guid gameId, Guid playerId, int row, int col);
        Task<TicTacToe.Api.Models.Game?> GetGameAsync(Guid id);
        Task<IEnumerable<TicTacToe.Api.Models.Game>> GetGamesByUserAsync(Guid userId);
        Task DeleteUnfinishedGamesAsync(Guid userId);
    }
}