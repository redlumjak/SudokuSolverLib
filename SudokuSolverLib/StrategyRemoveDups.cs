using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// Makes sure that the notes for each cell don't contain the same values that have already 
    /// been guessed from rows, columns, and areas
    /// </summary>
    public class StrategyRemoveDups : Runnable, Strategy
    {
        public string Key
        {
            get
            {
                return "RemoveDups";
            }
        }
        public string Description
        {
            get
            {
                return "Makes sure that the notes for each cell don't contain the same values that have already been guessed from rows, columns, and areas.";
            }
        }
        public Strategy Create()
        {
            return new StrategyRemoveDups();
        }

        public bool Run(Board board)
        {
            bool changed = false;

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    var cell = board.getAt(row, col);
                    if (cell.changed)
                    {
                        var value = cell.value;
                        foreach (BoardIndex index in board.GetRowColAreaEnumerator(row, col))
                        {
                            if (board.getAt(index.row, index.col).notes.HasValue(value))
                            {
                                changed = true;
                                board.setNotesRemove(index, value);
                            }
                        }
                    }
                }
            }
            return changed;
        }

    }
}
