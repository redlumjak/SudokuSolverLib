using SudokuSolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    /// <summary>
    /// This is a tricky strategy. If there are 2 cells with just two numbers, then those numbers can
    /// be removed from all other cells in the group. Similarly with 3 cells and 3 numbers, or 4 cells 
    /// with 4 numbers. 
    /// The maximum number is the number of unknown cells minus 2, because if we have all but one cell
    /// then it would've been caught by StrategySingle. 
    /// TODO Should we replace StrategySingle with this class? 
    /// 
    /// Algorithm: 
    /// numUnknowns = number of unknowns
    /// maxSize = numUnknowns - 2
    /// mapPossibles is map of possible pairs
    /// foreach cell
    ///     if notes are too large
    ///         continue
    ///     if notes are in mapPossibles
    ///          add to mapPossibles
    ///     foreach key in mapPossibles
    ///         add union to mapPossibles
    /// foreach key in mapPossibles
    ///     if number of indexes equals the number of notes
    ///         This is a pair. Remove notes from all other cells
    /// </summary>
    public class StrategyPairs : Runnable, Strategy
    {
        public string Key 
        { 
            get 
            { 
                return "Pairs"; 
            } 
        }
        public string Description
        {
            get
            {
                return "If there are 2 cells with just two numbers, then those numbers can be removed from all other cells in the group. Similarly with 3 cells and 3 numbers, or 4 cells with 4 numbers. The maximum number is the number of unknown cells minus 2, because if we have all but one cell then it would've been caught by StrategySingle. ";
            }
        }
        public bool Run(Board board)
        {
            bool changed = false;

            for (int idx = 0; idx < 9; idx++)
            {
                bool rowChanged = Run(board, board.GetRowEnumerator(idx));
                bool colChanged = Run(board, board.GetColEnumerator(idx));
                bool areaChanged = Run(board, board.GetAreaEnumerator(idx));
                changed = changed || rowChanged || colChanged || areaChanged;
            }

            return changed;
        }

        public Strategy Create()
        {
            return new StrategyPairs();
        }

        internal bool Run(Board board, IEnumerable<BoardIndex> enumerator)
        {
            bool changed = false;

            int numUnknowns = getNumUnknowns(board, enumerator);
            int maxSize = numUnknowns - 2;
            var mapPossibles = new Dictionary<int, List<BoardIndex>>();
            foreach (var index in enumerator)
            {
                var cell = board.getAt(index);
                var bits = cell.notes.bits;
                //     if notes are too large
                //         continue
                if (cell.notes.Count() > maxSize)
                {
                    continue;
                }
                //     if notes are in mapPossibles
                //          add to mapPossibles
                if (mapPossibles.ContainsKey(bits))
                {
                    mapPossibles[bits].Add(index);
                }
                //     foreach key in mapPossibles
                //         add union to mapPossibles
                foreach (var pair in mapPossibles)
                {
                    var union = pair.Key | bits;
                    if (PencilMarks.CountBits(union) > maxSize)
                    {
                        continue;
                    }
                    if (mapPossibles.ContainsKey(union))
                    {
                        mapPossibles[union].Add(index);
                    }
                    else
                    {
                        mapPossibles[union] = new List<BoardIndex>() { index };
                    }
                }
            }

            // foreach key in mapPossibles
            //     if number of indexes equals the number of notes
            //         This is a pair. Remove notes from all other cells
            foreach (var item in mapPossibles)
            {
                var numBits = PencilMarks.CountBits(item.Key);
                if (item.Value.Count() == numBits)
                {
                    if (RemoveNotes(board, item.Key, item.Value, enumerator))
                    {
                        changed = true;
                    }
                }
            }

            return changed;
        }

        private bool RemoveNotes(Board board, int bits, List<BoardIndex> boardIndices, IEnumerable<BoardIndex> enumerator)
        {
            var changed = false;

            foreach (var index in enumerator)
            {
                if (!boardIndices.Contains(index))
                {
                    if ((board.getAt(index).notes.bits & bits) != 0)
                    {
                        changed = true;
                        board.setNotesRemoveBits(index, bits);
                    }
                }
            }

            return changed;
        }

        private int getNumUnknowns(Board board, IEnumerable<BoardIndex> enumerator)
        {
            int numUnknowns = 0;
            foreach (var index in enumerator)
            {
                if (board.getAt(index).value == 0)
                {
                    numUnknowns++;
                }
            }
            return numUnknowns;
        }
    }
}
