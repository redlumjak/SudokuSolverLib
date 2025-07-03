using SudokuSolver;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// Contains a single sudoku board. By default this is the 9 by 9 grid of cells. 
    /// </summary>
    public class Board
    {
        [JsonInclude]
        private Cell[, ] items = new Cell[9, 9];
        /// <summary>
        /// default constructor
        /// </summary>
        public Board() 
        {
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    items[row, col] = new Cell();
                }
            }
        }

        /// <summary>
        /// creates a board from a json string
        /// </summary>
        /// <param name="json">a json string representing a board</param>
        /// <returns>a new board from the json string</returns>
        /// <exception cref="Exception"></exception>
        static public Board FromJson(string json) 
        {
            var theMap = JsonSerializer.Deserialize<Dictionary<string, Cell>>(json);
            if (theMap == null)
            {
                throw new NullReferenceException("invalid json");
            }
            Board board = new Board();
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    string key = row.ToString() + col.ToString();
                    board.setAt(row, col, theMap[key]);
                }
            }
            return board;
        }

        /// <summary>
        /// returns a json string representing the board
        /// </summary>
        /// <returns>a json string</returns>
        public string toJson()
        {
            // I can't serialize a 2d array, so I'll convert items to a map
            var theMap = new Dictionary<string, Cell>();
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    string key = row.ToString() + col.ToString();
                    theMap[key] = items[row, col];
                }
            }
            return JsonSerializer.Serialize(theMap);
        }
        /// <summary>
        /// sets cell at current location. Used for initializing. 
        /// </summary>
        /// <param name="row">zero based row index</param>
        /// <param name="col">zero based column index</param>
        /// <param name="cell">cell to replace existing cell</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException">if row or col is less than zero or greater than 8</exception>
        public void setAt(int row, int col, Cell cell) { 
            if (cell == null)
            {
                throw new ArgumentNullException("cell");
            }
            if (row < 0 || col < 0 || row > 8 || col > 8)
            { 
                throw new ArgumentOutOfRangeException();
            }
            items[row,col] = cell;
        }
        public void setValue(BoardIndex index, int value)
        {
            items[index.row, index.col].value = value;
            fixPencilMarks(index);
        }
        public void setMarks(BoardIndex index, PencilMarks marks)
        {
            items[index.row, index.col].notes.bits = marks.bits;
        }
        public void setNotesRemoveBits(BoardIndex index, int bits)
        {
            items[index.row, index.col].notes.RemoveBits(bits);
        }
        public void setNotesRemove(BoardIndex index, int value)
        {
            items[index.row, index.col].notes.Remove(value);
        }
        public void setNotesRemove(BoardIndex index, PencilMarks marks)
        {
            items[index.row, index.col].notes.Remove(marks);
        }
        /// <summary>
        /// Returns copy of cell at the given location
        /// </summary>
        /// <param name="row">zero based row index</param>
        /// <param name="col">zero based column index</param>
        /// <returns></returns>
        public Cell getAt(int row, int col) 
        { 
            return items[row,col]; 
        }
        public Cell getAt(BoardIndex index)
        {
            return getAt(index.row, index.col);
        }
        /// <summary>
        /// Notes in all cells have all 9 pencil marks set
        /// </summary>
        public void setAllNotes()
        {
            foreach (var item in items)
            {
                if (item.value == 0)
                {
                    item.notes.SetAll();
                }
                else
                {
                    item.notes.bits = 0;
                }
            }
        }

        public void fixAllPencilMarks()
        {
            foreach (var index in GetBoardEnumerator())
            {
                fixPencilMarks(index);
            }
        }

        public void fixPencilMarks(BoardIndex indexValue)
        {
            var row = indexValue.row;
            var col = indexValue.col;
            var value = getAt(row,col).value;
            if (value == 0)
            {
                return;
            }
            items[row, col].notes.bits = 0;
            foreach (BoardIndex index in GetRowColAreaEnumerator(row, col))
            {
                if (getAt(index.row, index.col).notes.HasValue(value))
                {
                    setNotesRemove(index, value);
                }
            }

        }

        /// <summary>
        /// Iterates over all cells in the board.
        /// </summary>
        /// <returns></returns>
        public BoardEnumerator GetBoardEnumerator() => new BoardEnumerator(this);

        /// <summary>
        /// Can be used to iterate over all cells in the given row
        /// </summary>
        /// <param name="row">zero based row index</param>
        /// <returns>enumerator for iterating over a row</returns>
        public RowEnumerator GetRowEnumerator(int row) => new RowEnumerator(this, row);

        /// <summary>
        /// Can be used to iterate over all cells in the given column
        /// </summary>
        /// <param name="col">zero based column index</param>
        /// <returns>enumerator for iterating over a column</returns>
        public ColEnumerator GetColEnumerator(int col) => new ColEnumerator(this, col);

        /// <summary>
        /// Can be used to iterate over all cells in the given area. 
        /// </summary>
        /// <param name="area">Area 0 is the top left, 1 is top middle, etc.</param>
        /// <returns>enumerator for iterating over an area</returns>
        public AreaEnumerator GetAreaEnumerator(int area) => new AreaEnumerator(this, area);

        public ColRemainderEnumerator GetColRemainderEnumerator(int col, int area) => new ColRemainderEnumerator(this, col, area);

        public RowRemainderEnumerator GetRowRemainderEnumerator(int row, int area) => new RowRemainderEnumerator(this, row, area);

        public RowColAreaEnumerator GetRowColAreaEnumerator(int row, int col) => new RowColAreaEnumerator(this, row, col);

        /// <summary>
        /// returns the row offset for each area. 
        /// </summary>
        /// <param name="area"></param>
        public int RowOffsetForArea(int area) => ((int)(area / 3)) * 3;

        /// <summary>
        /// returns the column offset for each area. 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public int ColOffsetForArea(int area) => (area % 3) * 3;
        /// <summary>
        /// returns an area index for the passed in location. If the location is within area 0, then 0 is returned. 
        /// </summary>
        /// <param name="row">zero based row index</param>
        /// <param name="col">zero based column index</param>
        /// <returns>area index</returns>
        static public int AreaFromIndex(int row, int col)
        {
            return ((int)(row / 3)) * 3 + ((int)col / 3);
        }

        public void Validate()
        {
            for (int idx = 0; idx < 9;  idx++)
            {
                Validate(this.GetRowEnumerator(idx));
                Validate(this.GetColEnumerator(idx));
                Validate(this.GetAreaEnumerator(idx));
            }
        }

        public bool IsComplete()
        {
            foreach (var index in this.GetBoardEnumerator())
            {
                if (this.getAt(index).value == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void Validate(IEnumerable<BoardIndex> enumerator)
        {
            PencilMarks marks = new PencilMarks();
            foreach (var index in enumerator)
            {
                var cell = items[index.row, index.col];
                if (cell.value != 0)
                {
                    if (marks.HasValue(cell.value))
                    {
                        throw new Exception("Duplicate value");
                    }
                    marks.Add(cell.value);
                    if (cell.notes.bits != 0)
                    {
                        throw new Exception("Cells with values should not have notes");
                    }
                }
                else
                {
                    if (cell.notes.bits == 0)
                    {
                        throw new Exception("Invalid pencil marks");
                    }
                }
            }
        }
    }
}
