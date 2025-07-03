using SudokuSolverApi.Models;
using SudokuSolver;

namespace SudokuSolverApi
{
    /// <summary>
    /// This is the class that loops through the various strategies to solve the sudoku
    /// </summary>
    public class Solver
    {
        List<Strategy> _strategies;
        Board _board;
        Game _game;

        private int _turnNumber;
        public Solver(Game game)
        {
            _game = game;
            _strategies = new Strategies().GetStrategies(game.Strategies);
            _board = game.MakeBoard();
        }
        public void Solve()
        {
            Console.WriteLine("Entered Solve");
            try
            {
                _board.setAllNotes();
                _board.fixAllPencilMarks();
                do
                {
                    takeATurn();
                } while (!_board.IsComplete());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // TODO save exception to DB
            }
            Console.WriteLine("Leaving Solve");
        }
        private void takeATurn()
        {
            foreach (var strategy in _strategies)
            {
                Console.WriteLine("strategy: " + strategy.Key);
                var changed = strategy.Run(_board);
                if (changed)
                {
                    _turnNumber++;
                    var turn = new Turn(_game.Id, _turnNumber, _board, strategy.Key);
                    Console.WriteLine("Turn: " + _turnNumber);
                    Console.WriteLine("strategy: " + turn.Strategy);
                    Console.WriteLine(_board.toJson());
                    // TODO save to DB
                    return;
                }
            }
            // no changes and the board isn't finished
            throw new Exception("Could not solve puzzle");
        }
    }
}
