using SudokuSolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class RowEnumerator : IEnumerable<BoardIndex>
    {
        private Board board;
        private int row {  get; set; }

        public RowEnumerator(Board board, int row)
        {
            this.board = board;
            this.row = row;
        }

        public IEnumerator<BoardIndex> GetEnumerator()
        {
            var index = new BoardIndex();
            index.row = row;
            for (index.col = 0; index.col < 9; index.col++)
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
