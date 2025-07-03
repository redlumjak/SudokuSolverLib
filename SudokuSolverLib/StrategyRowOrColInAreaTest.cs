using NUnit.Framework;
using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class StrategyRowOrColInAreaTest
    {
        private Board board;

        public StrategyRowOrColInAreaTest()
        {
            board = new Board();
        }

        [SetUp]
        public void Setup()
        {
            // 1 is unique in row 2 of area 0
            // 2 is unique in col 0 of area 0
            board.setAllNotes();
            var marks = new PencilMarks();
            marks.Set(new List<int> { 2, 3, 4 });
            board.setMarks(new BoardIndex(0, 0), marks);
            board.setMarks(new BoardIndex(1, 0), marks);
            marks.Add(1);
            board.setMarks(new BoardIndex(2, 0), marks);

            marks.Set(new List<int> { 1, 3, 4 });
            board.setMarks(new BoardIndex(2, 1), marks);
            board.setMarks(new BoardIndex(2, 2), marks);

            // the rest of the area
            marks.Set(new List<int> { 3, 4, 5, 6, 7, 8, 9 });
            board.setMarks(new BoardIndex(0, 1), marks);
            board.setMarks(new BoardIndex(0, 2), marks);
            board.setMarks(new BoardIndex(1, 1), marks);
            board.setMarks(new BoardIndex(1, 2), marks);
        }

        [Test]
        public void TestHappyPath()
        {
            StrategyRowOrColInArea strategy = new StrategyRowOrColInArea();
            var changed = strategy.Run(board);
            Assert.That(changed, Is.True);

            Assert.That(board.getAt(3, 0).notes.HasValue(2), Is.EqualTo(false));
            Assert.That(board.getAt(4, 0).notes.HasValue(2), Is.EqualTo(false));
            Assert.That(board.getAt(5, 0).notes.HasValue(2), Is.EqualTo(false));
            Assert.That(board.getAt(6, 0).notes.HasValue(2), Is.EqualTo(false));
            Assert.That(board.getAt(7, 0).notes.HasValue(2), Is.EqualTo(false));
            Assert.That(board.getAt(8, 0).notes.HasValue(2), Is.EqualTo(false));

            Assert.That(board.getAt(2, 3).notes.HasValue(1), Is.EqualTo(false));
            Assert.That(board.getAt(2, 4).notes.HasValue(1), Is.EqualTo(false));
            Assert.That(board.getAt(2, 5).notes.HasValue(1), Is.EqualTo(false));
            Assert.That(board.getAt(2, 6).notes.HasValue(1), Is.EqualTo(false));
            Assert.That(board.getAt(2, 7).notes.HasValue(1), Is.EqualTo(false));
            Assert.That(board.getAt(2, 8).notes.HasValue(1), Is.EqualTo(false));
        }

    }
}
