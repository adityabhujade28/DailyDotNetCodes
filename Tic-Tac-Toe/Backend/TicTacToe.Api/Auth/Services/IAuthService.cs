using System;
using System.Threading.Tasks;
using TicTacToe.Api.Auth.Models;

namespace TicTacToe.Api.Auth.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string username, string password);
        Task<User?> ValidateCredentialsAsync(string username, string password);
    }
}