using System.Threading.Tasks;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string username, string password);
        Task<string?> LoginAsync(string username, string password);
    }
}