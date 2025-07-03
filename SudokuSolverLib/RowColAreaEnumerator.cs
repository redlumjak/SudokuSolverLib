using SudokuSolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class RowColAreaEnumerator : IEnumerable<BoardIndex>
    {
        private Board board;
        private int row { get; set; }
        private int col { get; set; }
        // public int area { get; private set; }

        public RowColAreaEnumerator(Board board, int row, int col)
        {
            this.board = board;
            this.row = row;
            this.col = col;
        }

        public IEnumerator<BoardIndex> GetEnumerator()
        {
            var area = Board.AreaFromIndex(row, col);
            foreach (BoardIndex index in board.GetAreaEnumerator(area))
            {
                yield return index;
            }
            foreach (BoardIndex index in board.GetRowRemainderEnumerator(row, area))
            {
                yield return index;
            }
            foreach (BoardIndex index in board.GetColRemainderEnumerator(col, area))
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
