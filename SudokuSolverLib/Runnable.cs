using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// used to return both a cell and a bool to say whether the cell changed.
    /// </summary>
    public struct ChangedCell
    {
        public Cell? cell;
        public bool changed;
    }
    public interface Runnable
    {
        /// <summary>
        /// runs a strategy on the given board. 
        /// </summary>
        /// <returns>true if a change was made. False otherwise. </returns>
        bool Run(Board board);
    }

}
