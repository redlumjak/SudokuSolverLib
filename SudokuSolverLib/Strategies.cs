using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SudokuSolver
{
    /// <summary>
    /// Lists all strategies and allows manipulation of them
    /// </summary>
    public class Strategies
    {
        private List<Strategy> AllStrategies { get; }

        public Strategies()
        {
            this.AllStrategies = new List<Strategy>();
            AllStrategies.Add(new StrategySingle());
            AllStrategies.Add(new StrategyUnique());
            AllStrategies.Add(new StrategyRowOrColInArea());
            AllStrategies.Add(new StrategyPairs());
        }

        public List<Strategy> GetStrategies(string[] keys)
        {
            var strategies = new List<Strategy>();
            foreach (var key in keys)
            {
                if (InvalidKey(key))
                {
                    throw new InvalidOperationException(key);
                }
            }
            // AllStrategies has the strategies in the order that I want to run them. 
            foreach (var strategy in AllStrategies)
            {
                if (keys.Contains(strategy.Key))
                {
                    strategies.Add(strategy);
                }
            }
            return strategies;
        }

        private bool InvalidKey(string key)
        {
            foreach (var strategy in this.AllStrategies)
            {
                if (strategy.Key == key)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
