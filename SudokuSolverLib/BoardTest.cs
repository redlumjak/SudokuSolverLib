using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SudokuSolver
{
    public class BoardTest
    {
        Board board = new Board();

        [SetUp]
        public void Setup()
        {
            board = initBoard();
        }

        static public Board initBoard()
        {
            var board = new Board();
            board.setAllNotes();
            board.setAt(0, 1, new Cell(2, Cell.CellType.given));
            board.setAt(0, 6, new Cell(3, Cell.CellType.guessed));
            board.setAt(1, 0, new Cell(4, Cell.CellType.guessed));
            return board;
        }

        /// <summary>
        /// makes sure that toJson and fromJson work
        /// </summary>
        [Test]
        public void TestJson()
        {
            using (var test_Stream = new MemoryStream())
            {
                var json = board.toJson();
                var board2 = Board.FromJson(json);

                // Assert    
                Assert.That(board2, Is.Not.Null);
                AssertDefaultBoard(board2);
            }
        }

        [Test]
        public void TestSetup()
        {
            AssertDefaultBoard(board);
        }

        /// <summary>
        /// makes sure the board passed in is the same as the default board
        /// </summary>
        /// <param name="board"></param>
        private void AssertDefaultBoard(Board board)
        {
            Assert.That(board.getAt(0, 1).value, Is.EqualTo(2));
            Assert.That(board.getAt(0, 1).cellType, Is.EqualTo(Cell.CellType.given));
            Assert.That(board.getAt(0, 6).value, Is.EqualTo(3));
            Assert.That(board.getAt(0, 6).cellType, Is.EqualTo(Cell.CellType.guessed));
            Assert.That(board.getAt(1, 0).value, Is.EqualTo(4));
            Assert.That(board.getAt(1, 0).cellType, Is.EqualTo(Cell.CellType.guessed));
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 3)]
        [TestCase(2, 0, 6)]
        [TestCase(3, 3, 0)]
        [TestCase(4, 3, 3)]
        [TestCase(5, 3, 6)]
        [TestCase(6, 6, 0)]
        [TestCase(7, 6, 3)]
        [TestCase(8, 6, 6)]
        public void TestAreaOffsets(int area, int row, int col)
        {
            Assert.That(board.RowOffsetForArea(area), Is.EqualTo(row));
            Assert.That(board.ColOffsetForArea(area), Is.EqualTo(col));
        }
        [TestCase(2, 2, 0)]
        [TestCase(0, 0, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(2, 4, 1)]
        [TestCase(1, 5, 1)]
        [TestCase(0, 3, 1)]
        [TestCase(0, 6, 2)]
        [TestCase(1, 7, 2)]
        [TestCase(2, 8, 2)]
        [TestCase(3, 6, 5)]
        [TestCase(4, 5, 4)]
        [TestCase(5, 1, 3)]
        [TestCase(6, 0, 6)]
        [TestCase(7, 4, 7)]
        [TestCase(8, 8, 8)]
        public void TestAreaFromIndex(int row, int col, int area)
        {
            Assert.That(Board.AreaFromIndex(row, col), Is.EqualTo(area));
        }
        [Test]
        public void TestSetAllNotes()
        {
            var allMarks = new PencilMarks();
            allMarks.Add(1);
            allMarks.Add(2);
            allMarks.Add(3);
            allMarks.Add(4);
            allMarks.Add(5);
            allMarks.Add(6);
            allMarks.Add(7);
            allMarks.Add(8);
            allMarks.Add(9);
            board.setAllNotes();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    Assert.That(board.getAt(row, col).notes.bits, Is.EqualTo(allMarks.bits));
                    Assert.That(board.getAt(row, col).notes, Is.EqualTo(allMarks));
                }
            }
        }
        [Test]
        public void TestValidate()
        {
            // TODO There are three cases, and all three should be tested. 
            Assert.Fail("Implement me");
        }
    }
}
