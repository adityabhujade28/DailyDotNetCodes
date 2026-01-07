using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Game.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Api.Services;

namespace TicTacToe.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private static ConcurrentDictionary<Guid, Game> Games = new();
        private readonly IUserStore _users;
        public GameController(IUserStore users) { _users = users; }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateGameRequest req)
        {
            var g = new Game { PlayerXId = req.PlayerXId, PlayerOId = req.PlayerOId };
            Games[g.Id] = g;
            return Ok(new { g.Id });
        }

        [HttpPost("{id}/move")]
        public async Task<IActionResult> Move(Guid id, [FromBody] MakeMoveRequest req)
        {
            if (!Games.TryGetValue(id, out var g)) return NotFound();
            var symbol = g.PlayerXId == req.PlayerId ? PlayerSymbol.X : PlayerSymbol.O;
            var ok = g.MakeMove(req.Row, req.Col, symbol);
            if (!ok) return BadRequest(new { error = "invalid move" });
            if (g.IsFinished && g.Winner != PlayerSymbol.None)
            {
                var winnerId = g.Winner == PlayerSymbol.X ? g.PlayerXId : g.PlayerOId;
                await _users.IncrementWinsAsync(winnerId);
            }
            return Ok(new { g.IsFinished, g.Winner });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (!Games.TryGetValue(id, out var g)) return NotFound();
            return Ok(g);
        }
    }

    public record CreateGameRequest(Guid PlayerXId, Guid PlayerOId);
    public record MakeMoveRequest(Guid PlayerId, int Row, int Col);
}