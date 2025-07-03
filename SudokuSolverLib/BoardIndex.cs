using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class BoardIndex
    {
        public int row;
        public int col;

        public BoardIndex()
        {

        }

        public BoardIndex(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
}
