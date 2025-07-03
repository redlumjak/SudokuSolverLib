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
    /// If a value in the notes exists only in one row in an area, then those values
    /// need to be removed from the notes in the rest of the row. Ditto for the columns. 
    /// </summary>
    public class StrategyRowOrColInArea : Runnable, Strategy
    {
        public string Key
        {
            get
            {
                return "RowOrColInArea";
            }
        }
        public string Description
        {
            get
            {
                return "If a value in the notes exists only in one row in an area, then those values need to be removed from the notes in the rest of the row. Ditto for the columns.";
            }
        }
        public Strategy Create()
        {
            return new StrategyRowOrColInArea();
        }


        public bool Run(Board board)
        {
            bool changed = false;

            for (var area = 0; area < 9; area++)
            {
                var rowOffset = board.RowOffsetForArea(area);
                var colOffset = board.ColOffsetForArea(area);
                for (var idx = 0; idx < 3; idx++)
                {
                    // combine all notes in row and rest of area
                    var rowNotes = new PencilMarks();
                    var colNotes = new PencilMarks();
                    var rowRemainderNotes = new PencilMarks();
                    var colRemainderNotes = new PencilMarks();
                    foreach (var index in board.GetAreaEnumerator(area))
                    {
                        var notes = board.getAt(index.row, index.col).notes;
                        if (index.row == rowOffset + idx)
                        {
                            rowNotes.Add(notes);
                        }
                        else
                        {
                            rowRemainderNotes.Add(notes);
                        }
                        if (index.col == colOffset + idx)
                        {
                            colNotes.Add(notes); 
                        }
                        else
                        {
                            colRemainderNotes.Add(notes);
                        }
                    }
                    // remove notes from rest of area
                    rowNotes.Remove(rowRemainderNotes);
                    colNotes.Remove(colRemainderNotes);
                    // if any notes remain, remove them from rest of row
                    if (!rowNotes.IsEmpty())
                    {
                        foreach (var index in board.GetRowRemainderEnumerator(rowOffset + idx, area))
                        {
                            if (board.getAt(index.row, index.col).notes.HasValue(rowNotes))
                            {
                                changed = true;
                                board.setNotesRemove(index, rowNotes);
                            }
                        }
                    }
                    if (!colNotes.IsEmpty()) 
                    {
                        foreach (var index in board.GetColRemainderEnumerator(colOffset + idx, area))
                        {
                            if (board.getAt(index.row, index.col).notes.HasValue(colNotes))
                            {
                                changed = true;
                                board.setNotesRemove(index, colNotes);
                            }
                        }
                    }
                }
            }

            return changed;
        }

    }
}
