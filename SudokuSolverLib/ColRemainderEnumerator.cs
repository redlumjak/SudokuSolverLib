using SudokuSolver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class ColRemainderEnumerator : IEnumerable<BoardIndex>
    {
        private Board board;
        private int col { get; set; }
        private int area {  get; set; }

        public ColRemainderEnumerator(Board board, int col, int area)
        {
            this.board = board;
            this.col = col;
            this.area = area;
        }

        public IEnumerator<BoardIndex> GetEnumerator()
        {
            var index = new BoardIndex();
            index.col = col;
            int rowOffset = board.RowOffsetForArea(area);
            for (index.row = 0; index.row < 9; index.row++)
            {
                // ignore rows within the area
                if (index.row >= rowOffset && index.row < rowOffset + 3)
                {
                    index.row += 2;
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
