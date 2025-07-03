using SudokuSolver;

namespace SudokuSolverApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int[][] values { get; set; }
        public string[] Strategies { get; set; }

        public Game()
        {
            this.Description = string.Empty;
            this.values = new int[9][];
            for (int i = 0; i<9; i++)
            {
                values[i] = new int[9];
            }
            this.Strategies = new string[] { };
        }

        public Game(int id, string description, int[][] values, string[] strategies)
        {
            this.Id = id;
            this.Description = description;
            this.values = values;
            this.Strategies = strategies;
        }

        public Game(string description, int[][] values, string[] strategies)
        {
            this.Description = description;
            this.values = values;
            this.Strategies = strategies;
        }

        public Board MakeBoard()
        {
            Board board = new Board();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (values[row][col] > 0)
                    {
                        Cell cell = new Cell();
                        cell.value = values[row][col];
                        cell.cellType = Cell.CellType.given;
                        cell.changed = true;
                        board.setAt(row, col, cell);
                    }
                }
            }
            return board;
        }
    }
}
