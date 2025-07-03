using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SudokuSolver;

namespace SudokuSolver
{
    public class ColEnumeratorTest
    {
        private Board board;

        public ColEnumeratorTest()
        {  
            board = new Board(); 
        }

        [SetUp]
        public void Setup()
        {
            board = BoardTest.initBoard();
            board.setAt(3, 2, new Cell(9, Cell.CellType.given));
            board.setAt(6, 2, new Cell(7, Cell.CellType.given));
        }

        [Test]
        public void TestRead()
        {
            int defaultCount = 0;
            int count1 = 0;
            int count2 = 0;
            ColEnumerator enumerator = board.GetColEnumerator(2);
            foreach (var index in enumerator)
            {
                var cell = board.getAt(index.row, index.col);
                Assert.That(index.col, Is.EqualTo(2));
                switch (index.row)
                {
                    case 3:
                        Assert.That(cell.value, Is.EqualTo(9));
                        Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.given));
                        count1++;
                        break;
                    case 6:
                        Assert.That(cell.value, Is.EqualTo(7));
                        Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.given));
                        count2++;
                        break;
                    default:
                        Assert.That(cell.value, Is.EqualTo(0));
                        Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.unknown));
                        defaultCount++;
                        break;
                }
            }
            Assert.That(count1, Is.EqualTo(1));
            Assert.That(count2, Is.EqualTo(1));
            Assert.That(defaultCount, Is.EqualTo(7));
        }

    }
}
