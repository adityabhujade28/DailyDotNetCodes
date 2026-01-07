
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
            var user = await _auth.RegisterAsync(req.Username, req.Password);
            if (user == null) return BadRequest(new { error = "User exists" });
            return CreatedAtAction(null, new { id = user.Id }, new { user.Id, user.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var token = await _auth.LoginAsync(req.Username, req.Password);
            if (token == null) return Unauthorized();
            return Ok(new { token });
        }
    }
}