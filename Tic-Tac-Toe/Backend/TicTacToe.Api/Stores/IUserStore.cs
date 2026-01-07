using System;
using System.Threading.Tasks;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Stores
{
    public interface IUserStore
    {
        Task<User?> GetByUsernameAsync(string username);
        Task CreateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task IncrementWinsAsync(Guid userId);
    }
}