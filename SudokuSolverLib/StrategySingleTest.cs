using NUnit.Framework;
using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class StrategySingleTest
    {
        private Board board;

        public StrategySingleTest()
        {
            board = new Board();
        }

        [SetUp]
        public void Setup()
        {
            board.setAllNotes();
            var marks = new PencilMarks();
            marks.Set(new List<int> { 3 });
            board.setMarks(new BoardIndex(5, 0), marks);
        }

        [Test]
        public void TestHappyPath()
        {
            StrategyUnique strategy = new StrategyUnique();
            var changed = strategy.Run(board);
            Assert.That(changed, Is.True);

            Assert.That(board.getAt(5, 0).value, Is.EqualTo(3));
        }

    }
}
