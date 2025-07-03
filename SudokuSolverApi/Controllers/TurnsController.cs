using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SudokuSolverApi.Models;

namespace SudokuSolverApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnsController : ControllerBase
    {
        private readonly TurnContext _context;

        public TurnsController(TurnContext context)
        {
            _context = context;
        }

        // GET: api/Turns/game/my-game-id
        // gets all turns for a particular gameId
        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<IEnumerable<Turn>>> GetTurn(long gameId)
        {
            return await _context.Turn
                .Where(turn => turn.GameId == gameId)
                .ToListAsync();
        }

        // GET: api/Turns/5
        [HttpGet("turn/{id}")]
        public async Task<ActionResult<Turn>> GetTurn(long gameId, long id)
        {
            var turn = await _context.Turn.FindAsync(id);

            if (turn == null)
            {
                return NotFound();
            }

            return turn;
        }

    }
}
