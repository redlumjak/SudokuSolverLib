using SudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public interface Strategy : Runnable
    {
        Strategy Create();
        string Key { get; }
        string Description { get; }
    }
}
