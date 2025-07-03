using Microsoft.EntityFrameworkCore;

namespace SudokuSolverApi.Models;

public class TurnContext : DbContext
{
    public TurnContext(DbContextOptions<TurnContext> options)
        : base(options)
    {
    }

    public DbSet<Turn> Turn { get; set; } = null!;
}
