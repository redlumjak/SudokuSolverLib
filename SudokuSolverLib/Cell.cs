using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// This is a single cell on the sudoku board. It has a value, and that value is either GIVEN at the beginning,
    /// UNKNOWN, or the value has been GUESSED. 
    /// </summary>
    public class Cell
    {
        public enum CellType 
        {
            unknown, 
            given, 
            guessed
        };
        private int myValue;
        /// <summary>
        /// the value. zero if it's still unknown
        /// </summary>
        [JsonInclude]
        public int value
        {
            set
            {
                myValue = value;
                changed = true;
            }
            get { return myValue; }
        }
        /// <summary>
        /// this is set to true before running the strategies and then each time a value is changed. 
        /// </summary>
        public bool changed;
        /// <summary>
        /// the type: unknown, given, guessed
        /// </summary>
        [JsonInclude]
        public CellType cellType;
        private PencilMarks myNotes;
        /// <summary>
        /// array of possible values
        /// </summary>
        [JsonInclude]
        public PencilMarks notes
        {
            set
            {
                myNotes = value;
                changed = true;
            }
            get { return myNotes; } 
        }

        public Cell(int value, CellType cellType)
        {
            this.myValue = value;
            this.cellType = cellType;
            this.myNotes = new PencilMarks();
            this.changed = true;
        }

        public Cell()
        {
            this.myValue = 0;
            this.cellType = CellType.unknown;
            this.myNotes = new PencilMarks();
            this.changed = false;
        }

        public override bool Equals(object? obj)
        {
            return obj is Cell cell &&
                   value == cell.value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value);
        }
    }
}
