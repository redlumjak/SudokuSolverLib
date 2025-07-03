using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SudokuSolver;

namespace SudokuSolver
{
    public class RowRemainderEnumeratorTest
    {
        private Board board;

        public RowRemainderEnumeratorTest()
        {
            board = new Board();
        }

        [SetUp]
        public void Setup()
        {
            board.setAt(2, 1, new Cell(1, Cell.CellType.given));
            board.setAt(5, 2, new Cell(2, Cell.CellType.given));
            board.setAt(5, 3, new Cell(3, Cell.CellType.given));
            board.setAt(5, 4, new Cell(4, Cell.CellType.given));
            board.setAt(5, 5, new Cell(5, Cell.CellType.given));
            board.setAt(5, 6, new Cell(6, Cell.CellType.given));
            board.setAt(5, 7, new Cell(7, Cell.CellType.given));
            board.setAt(5, 8, new Cell(8, Cell.CellType.given));
        }

        [TestCase(5, 3, 33, 0, 6)]
        [TestCase(5, 4, 23, 2, 4)]
        [TestCase(5, 5, 14, 2, 4)]
        public void TestRead(int row, int area, int expectedTotalValue, int expectedDefaultCount, int expectedCountValues)
        {
            int defaultCount = 0;
            int totalValue = 0;
            int countValues = 0;
            RowRemainderEnumerator enumerator = board.GetRowRemainderEnumerator(row, area);
            foreach (var index in enumerator)
            {
                var cell = board.getAt(index.row, index.col);
                Assert.That(index.row, Is.EqualTo(row));
                if (cell.cellType == Cell.CellType.unknown)
                {
                    defaultCount++;
                }
                else
                {
                    totalValue += cell.value;
                    countValues++;
                }
            }
            Assert.That(totalValue, Is.EqualTo(expectedTotalValue));
            Assert.That(countValues, Is.EqualTo(expectedCountValues));
            Assert.That(defaultCount, Is.EqualTo(expectedDefaultCount));
        }

    }
}
