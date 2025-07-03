using NUnit.Framework;
using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class StrategyUniqueTest
    {
        private Board board;

        public StrategyUniqueTest()
        {
            board = new Board();
        }

        [SetUp]
        public void Setup()
        {
            board.setAllNotes();
            var marks = new PencilMarks();
            marks.Set(new List<int> { 1, 2, 3 });
            board.setMarks(new BoardIndex(5, 0), marks);
            marks.bits = 0;
            marks.Set(new List<int> { 2, 3 } );
            board.setMarks(new BoardIndex(5, 1), marks);
            board.setMarks(new BoardIndex(5, 2), marks);
            marks.bits = 0;
            marks.Set(new List<int> { 4, 5, 6, 7, 8, 9 } );
            board.setMarks(new BoardIndex(5, 3), marks);
            board.setMarks(new BoardIndex(5, 4), marks);
            board.setMarks(new BoardIndex(5, 5), marks);
            board.setMarks(new BoardIndex(5, 6), marks);
            board.setMarks(new BoardIndex(5, 7), marks);
            board.setMarks(new BoardIndex(5, 8), marks);
        }

        [Test]
        public void TestHappyPath()
        {
            StrategyUnique strategy = new StrategyUnique();
            var changed = strategy.Run(board);
            Assert.That(changed, Is.True);

            Assert.That(board.getAt(5, 0).value, Is.EqualTo(1));
            // TODO add additional checks for single values in area, row, and col
        }

    }
}
