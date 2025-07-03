using SudokuSolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class AreaEnumerator : IEnumerable<BoardIndex>
    {
        private Board board;
        private int area;

        public AreaEnumerator(Board board, int area)
        {
            this.board = board;
            this.area = area;
        }

        public IEnumerator<BoardIndex> GetEnumerator()
        {
            int rowOffset = board.RowOffsetForArea(area);
            int colOffset = board.ColOffsetForArea(area);
            var index = new BoardIndex();
            for (index.row = rowOffset; index.row < rowOffset + 3; index.row++)
            {
                for (index.col = colOffset; index.col < colOffset + 3; index.col++)
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
