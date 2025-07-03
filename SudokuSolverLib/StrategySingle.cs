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
    /// If a notes field has just one value, then that's the right value 
    /// </summary>
    public class StrategySingle : Runnable, Strategy
    {
        public string Key
        {
            get
            {
                return "Single";
            }
        }
        public string Description
        {
            get
            {
                return "If a notes field has just one value, then that's the right value.";
            }
        }
        public Strategy Create()
        {
            return new StrategySingle();
        }

        public bool Run(Board board)
        {
            bool changed = false;

            foreach (var index in board.GetBoardEnumerator())
            {
                if (board.getAt(index).notes.IsSingleValue())
                {
                    changed = true;
                    board.setValue(index, board.getAt(index).notes.GetSingleValue());
                }
            }

            return changed;
        }

    }
}
