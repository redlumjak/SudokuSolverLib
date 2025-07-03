using SudokuSolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// This enumerator covers the entire board. Every row, every column. 
    /// </summary>
    public class BoardEnumerator : IEnumerable<BoardIndex>
    {
        private Board board;

        public BoardEnumerator(Board board)
        {
            this.board = board;
        }

        public IEnumerator<BoardIndex> GetEnumerator()
        {
            var index = new BoardIndex();
            for (index.row = 0; index.row < 9; index.row++)
            {
                for (index.col = 0; index.col < 9; index.col++)
                {
                    yield return index;
                }
            }

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
