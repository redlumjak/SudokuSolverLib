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
    public class GamesController : ControllerBase
    {
        private readonly GameContext _gameContext;

        public GamesController(GameContext context)
        {
            _gameContext = context;
        }

        //// GET: api/Games/Strategies
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Game>>> GetStrategies()
        //{
        //    return await _gameContext.Game.ToListAsync();
        //}

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            return await _gameContext.Game.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _gameContext.Game.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _gameContext.Game.Add(game);
            await _gameContext.SaveChangesAsync();

            new Solver(game).Solve();

            return CreatedAtAction("GetGame", new { id = game.Id }, game);
        }

    }
}
