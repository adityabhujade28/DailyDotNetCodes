

namespace TicTacToe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _game;
        public GameController(IGameService game) { _game = game; }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateGameRequest req)
        {
            var g = await _game.CreateGameAsync(req.PlayerXId, req.PlayerOId);
            return Ok(new { g.Id });
        }

        [HttpPost("{id}/move")]
        public async Task<IActionResult> Move(Guid id, [FromBody] MakeMoveRequest req)
        {
            var res = await _game.MakeMoveAsync(id, req.PlayerId, req.Row, req.Col);
            if (res == null) return BadRequest(new { error = "invalid move or game not found" });
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var g = await _game.GetGameAsync(id);
            if (g == null) return NotFound();
            var jagged = AppDbContext.ToJagged(g.Board.Cells);
            return Ok(new GameStateResponse(g.Id, jagged, g.Turn, g.IsFinished, g.Winner));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var list = await _game.GetGamesByUserAsync(userId);
            var dto = list.Select(g => new GameStateResponse(g.Id, AppDbContext.ToJagged(g.Board.Cells), g.Turn, g.IsFinished, g.Winner));
            return Ok(dto);
        }

        [HttpDelete("user/{userId}/unfinished")]
        public async Task<IActionResult> DeleteUnfinished(Guid userId)
        {
            await _game.DeleteUnfinishedGamesAsync(userId);
            return Ok();
        }
    }
}