using SudokuSolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class ColEnumerator : IEnumerable<BoardIndex>
    {
        private Board board;
        private int col { get; set; }

        public ColEnumerator(Board board, int col)
        {
            this.board = board;
            this.col = col;
        }

        public IEnumerator<BoardIndex> GetEnumerator()
        {
            BoardIndex index = new BoardIndex();
            index.col = this.col;
            for (index.row = 0; index.row < 9; index.row++)
            {
                yield return index;
            }

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
