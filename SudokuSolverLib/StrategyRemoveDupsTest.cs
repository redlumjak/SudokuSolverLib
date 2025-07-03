using NUnit.Framework;
using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class StrategyRemoveDupsTest
    {
        private Board board;

        public StrategyRemoveDupsTest()
        {
            board = new Board();
        }

        [SetUp]
        public void Setup()
        {
            board.setAllNotes();
            board.setAt(5, 8, new Cell(8, Cell.CellType.given));
            board.setAt(2, 2, new Cell(3, Cell.CellType.given));
        }

        [Test]
        public void TestHappyPath()
        {
            StrategyRemoveDups strategy = new StrategyRemoveDups();
            var changed = strategy.Run(board);
            Assert.That(changed, Is.True);
            var missing38 = new PencilMarks();
            missing38.SetAll();
            missing38.Remove(3);
            missing38.Remove(8);
            Assert.That(board.getAt(5, 2).notes.bits, Is.EqualTo(missing38.bits));
            Assert.That(board.getAt(5, 2).notes, Is.EqualTo(missing38));
            // TODO add additional checks for single values in area, row, and col
        }

    }
}
