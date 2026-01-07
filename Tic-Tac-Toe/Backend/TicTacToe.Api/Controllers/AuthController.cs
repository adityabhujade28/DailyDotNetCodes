using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Auth.Services;
using TicTacToe.Api.Auth.Models;
using System.Threading.Tasks;

namespace TicTacToe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) { _auth = auth; }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            try
            {
                var user = await _auth.RegisterAsync(req.Username, req.Password);
                return CreatedAtAction(null, new { id = user.Id }, new { user.Id, user.Username });
            }
            catch (System.InvalidOperationException e) { return BadRequest(new { error = e.Message }); }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _auth.ValidateCredentialsAsync(req.Username, req.Password);
            if (user == null) return Unauthorized();
            // For simplicity: return user id as token placeholder
            return Ok(new { token = user.Id });
        }
    }

    public record RegisterRequest(string Username, string Password);
    public record LoginRequest(string Username, string Password);
}