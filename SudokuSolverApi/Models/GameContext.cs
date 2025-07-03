using Microsoft.EntityFrameworkCore;

namespace SudokuSolverApi.Models;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Game { get; set; } = null!;
}
