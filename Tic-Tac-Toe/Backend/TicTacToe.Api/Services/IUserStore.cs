using System;
using System.Threading.Tasks;
using TicTacToe.Api.Auth.Models;

namespace TicTacToe.Api.Services
{
    public interface IUserStore
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task IncrementWinsAsync(Guid userId);
    }
}