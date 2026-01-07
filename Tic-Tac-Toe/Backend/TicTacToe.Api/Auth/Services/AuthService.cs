using System.Threading.Tasks;
using TicTacToe.Api.Auth.Models;
using TicTacToe.Api.Services;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TicTacToe.Api.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserStore _store;
        public AuthService(IUserStore store) { _store = store; }

        public async Task<User> RegisterAsync(string username, string password)
        {
            var exists = await _store.GetByUsernameAsync(username);
            if (exists != null) throw new InvalidOperationException("User already exists");
            var user = new User { Username = username, PasswordHash = Hash(password) };
            return await _store.CreateUserAsync(user);
        }

        public Task<User?> ValidateCredentialsAsync(string username, string password)
        {
            return _store.GetByUsernameAsync(username).ContinueWith(t => {
                var u = t.Result; if (u==null) return (User?)null; return Verify(password,u.PasswordHash) ? u : null; });
        }

        private static string Hash(string password)
        {
            byte[] salt = new byte[128/8];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256/8));
            return Convert.ToBase64String(salt) + ":" + hashed;
        }

        private static bool Verify(string password, string stored)
        {
            var parts = stored.Split(':');
            if (parts.Length!=2) return false;
            var salt = Convert.FromBase64String(parts[0]);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256/8));
            return hashed == parts[1];
        }
    }
}