using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SudokuSolver;

namespace SudokuSolver
{
    public class RowEnumeratorTest
    {
        private Board board;

        public RowEnumeratorTest()
        {
            board = new Board();
        }
        [SetUp]
        public void Setup() 
        { 
            board = BoardTest.initBoard();
        }

        [Test]
        public void TestRead()
        {
            int defaultCount = 0;
            int count1 = 0;
            int count2 = 0;
            RowEnumerator enumerator = board.GetRowEnumerator(0);
            foreach (var index in enumerator)
            {
                var cell = board.getAt(index.row, index.col);
                Assert.That(index.row, Is.EqualTo(0));
                switch (index.col)
                {
                    case 1: 
                        Assert.That(cell.value, Is.EqualTo(2)); 
                        Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.given));
                        count1++;
                        break;
                    case 6:
                        Assert.That(cell.value, Is.EqualTo(3));
                        Assert.That(cell.cellType, Is.EqualTo(Cell.CellType.guessed));
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
