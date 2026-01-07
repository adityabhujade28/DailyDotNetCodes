

namespace TicTacToe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserStore _store;
        public UsersController(IUserStore store) { _store = store; }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var u = await _store.GetByIdAsync(id);
            if (u == null) return NotFound();
            return Ok(new { u.Id, u.Username, u.TotalWins });
        }
    }
}