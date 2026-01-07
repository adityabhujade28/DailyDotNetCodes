using System.Threading.Tasks;
using TicTacToe.Api.Models;
using TicTacToe.Api.Services.Interfaces;
using TicTacToe.Api.Stores;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TicTacToe.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly TicTacToe.Api.Stores.IUserStore _store;
        public AuthService(TicTacToe.Api.Stores.IUserStore store) { _store = store; }

        public async Task<User?> RegisterAsync(string username, string password)
        {
            var existing = await _store.GetByUsernameAsync(username);
            if (existing != null) return null;
            var user = new User { Username = username, PasswordHash = Hash(password) };
            await _store.CreateAsync(user);
            return user;
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var u = await _store.GetByUsernameAsync(username);
            if (u == null) return null;
            if (!Verify(password, u.PasswordHash)) return null;
            // simple token: user id string (replace with JWT later)
            return u.Id.ToString();
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
            if (parts.Length != 2) return false;
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