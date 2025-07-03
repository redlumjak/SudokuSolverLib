using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SudokuSolver;

namespace SudokuSolver
{
    public class AreaEnumeratorTest
    {
        private Board board;

        public AreaEnumeratorTest()
        {
            board = new Board();
        }

        [SetUp]
        public void Setup()
        {
            board = new Board();
            board.setAt(0, 6, new Cell(3, Cell.CellType.guessed));
            board.setAt(0, 7, new Cell(9, Cell.CellType.given));
            board.setAt(0, 8, new Cell(7, Cell.CellType.given));
            board.setAt(1, 6, new Cell(6, Cell.CellType.guessed));
            board.setAt(2, 8, new Cell(4, Cell.CellType.guessed));
        }

        [Test]
        public void TestRead()
        {
            int defaultCount = 0;
            int row0 = 0;
            int row1 = 0;
            int row2 = 0;
            AreaEnumerator enumerator = board.GetAreaEnumerator(2);
            foreach (var index in enumerator)
            {
                var cell = board.getAt(index.row, index.col);
                if (index.row == 0 && index.col == 6)
                {
                    row0++;
                    Assert.That(cell.value, Is.EqualTo(3));
                    Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.guessed));
                }
                else if (index.row == 0 && index.col == 7)
                {
                    row0++;
                    Assert.That(cell.value, Is.EqualTo(9));
                    Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.given));
                }
                else if (index.row == 0 && index.col == 8)
                {
                    row0++;
                    Assert.That(cell.value, Is.EqualTo(7));
                    Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.given));
                }
                else if (index.row == 1 && index.col == 6)
                {
                    row1++;
                    Assert.That(cell.value, Is.EqualTo(6));
                    Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.guessed));
                }
                else if (index.row == 2 && index.col == 8)
                {
                    row2++;
                    Assert.That(cell.value, Is.EqualTo(4));
                    Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.guessed));
                }
                else 
                {
                    defaultCount++;
                    Assert.That(cell.value, Is.EqualTo(0));
                    Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.unknown));
                }
            }
            Assert.That(row0, Is.EqualTo(3));
            Assert.That(row1, Is.EqualTo(1));
            Assert.That(row2, Is.EqualTo(1));
            Assert.That(defaultCount, Is.EqualTo(4));
        }

    }
}
