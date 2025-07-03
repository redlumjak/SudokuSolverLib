using System.Xml.Serialization;
using SudokuSolver;

namespace SudokuSolverApi.Models
{
    public class Turn
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public long TurnNumber { get; set; }
        /// <summary>
        /// json string of board
        /// </summary>
        public string Board { get; set; }
        /// <summary>
        /// strategy that made the change in this turn
        /// </summary>
        public string Strategy { get; set; }
        public bool IsComplete { get; set; }

        public Turn(long gameId, long turnNumber, Board board, string strategyKey)
        { 
            this.GameId = gameId;
            this.TurnNumber = turnNumber;
            this.Board = board.toJson();
            this.Strategy = strategyKey;
            this.IsComplete = board.IsComplete();
        }
    }
}
