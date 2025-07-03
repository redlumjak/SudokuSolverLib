using SudokuSolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class RowRemainderEnumerator : IEnumerable<BoardIndex>
    {
        private Board board;
        private int row { get; set; }
        private int area { get; set; }

        public RowRemainderEnumerator(Board board, int row, int area)
        {
            this.board = board;
            this.row = row;
            this.area = area;
        }

        public IEnumerator<BoardIndex> GetEnumerator()
        {
            int colOffset = board.ColOffsetForArea(area);
            var index = new BoardIndex();
            index.row = this.row;
            for (index.col = 0; index.col < 9; index.col++)
            {
                // ignore rows within the area
                if (index.col >= colOffset && index.col < colOffset + 3)
                {
                    index.col += 2;
                    continue;
                }
                yield return index;
            }

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
