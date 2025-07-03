using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// If there is only one of a value in a row, column, or area, then that cell should have that value. 
    /// </summary>
    public class StrategyUnique : Runnable, Strategy
    {
        public string Key
        {
            get
            {
                return "Unique";
            }
        }
        public string Description
        {
            get
            {
                return "If there is only one of a value in a row, column, or area, then that cell should have that value.";
            }
        }
        public Strategy Create()
        {
            return new StrategyUnique();
        }

        public bool Run(Board board)
        {
            bool changed = false;

            for (int idx = 0; idx < 9; idx++)
            {
                if (runOne(board, board.GetRowEnumerator(idx)))
                {
                    changed = true;
                }
                if (runOne(board, board.GetColEnumerator(idx)))
                {
                    changed = true;
                }
                if (runOne(board, board.GetAreaEnumerator(idx)))
                {
                    changed = true;
                }
            }
            return changed;
        }

        private bool runOne(Board board, IEnumerable<BoardIndex> enumerator)
        {
            var uniqueNotes = GetUniqueNotes(board, enumerator);
            if (uniqueNotes.IsEmpty())
            {
                return false;
            }
            SetUniqueNotes(board, enumerator, uniqueNotes);
            return true;
        }
        private static void SetUniqueNotes(Board board, IEnumerable<BoardIndex> enumerator, PencilMarks uniqueNotes)
        {
            foreach (var index in enumerator)
            {
                var notes = board.getAt(index.row, index.col).notes;
                // if one of the unique notes is in notes, then set the appropriate value
                if (notes.HasValue(uniqueNotes))
                {
                    var commonBit = notes.bits & uniqueNotes.bits;
                    // TODO commonBit should be a single value. An exception is thrown here if it isn't. 
                    var val = PencilMarks.bitsToValue[commonBit];
                    board.setValue(index, val);
                }
            }
        }
        private static PencilMarks GetUniqueNotes(Board board, IEnumerable<BoardIndex> enumerator)
        {
            PencilMarks allNotes = new PencilMarks();
            PencilMarks uniqueNotes = new PencilMarks();
            foreach (var index in enumerator)
            {
                var notes = board.getAt(index.row, index.col).notes;
                // find notes that are not already in allNotes. Those are new. 
                PencilMarks newNotes = new PencilMarks(notes);
                newNotes.Remove(allNotes);

                // find notes that are already in allNotes. Those can be removed from uniqueNotes. 
                PencilMarks existingNotes = new PencilMarks(notes);
                existingNotes.Remove(newNotes);
                uniqueNotes.Remove(existingNotes);

                // add newNotes to allNotes and uniqueNotes
                allNotes.Add(newNotes);
                uniqueNotes.Add(newNotes);
            }
            return uniqueNotes;
        }
    }
}
