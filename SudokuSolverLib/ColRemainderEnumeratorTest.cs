using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SudokuSolver;

namespace SudokuSolver
{
    public class ColRemainderEnumeratorTest
    {
        private Board board;

        public ColRemainderEnumeratorTest()
        {
            board = new Board();
        }

        [SetUp]
        public void Setup()
        {
            board.setAt(2, 1, new Cell(1, Cell.CellType.given));
            board.setAt(2, 2, new Cell(2, Cell.CellType.given));
            board.setAt(3, 2, new Cell(3, Cell.CellType.given));
            board.setAt(4, 2, new Cell(4, Cell.CellType.given));
            board.setAt(5, 2, new Cell(5, Cell.CellType.given));
            board.setAt(6, 2, new Cell(6, Cell.CellType.given));
            board.setAt(7, 2, new Cell(7, Cell.CellType.given));
            board.setAt(8, 2, new Cell(8, Cell.CellType.given));
        }

        [TestCase(2, 0, 33, 0, 6)]
        [TestCase(2, 3, 23, 2, 4)]
        [TestCase(2, 6, 14, 2, 4)]
        public void TestRead(int col, int area, int expectedTotalValue, int expectedDefaultCount, int expectedCountValues)
        {
            int defaultCount = 0;
            int totalValue = 0;
            int countValues = 0;
            ColRemainderEnumerator enumerator = board.GetColRemainderEnumerator(col,area);
            foreach (var index in enumerator)
            {
                var cell = board.getAt(index.row, index.col);
                Assert.That(index.col, Is.EqualTo(col));
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
