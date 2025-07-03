namespace SudokuSolverApi.Models
{
    public class StartGame
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int[,] values { get; set; }

        public StartGame()
        {
            this.Description = string.Empty;
            this.values = new int[9, 9];
        }

        public StartGame(int id, string description, int[,] values)
        {
            this.Id = id;
            this.Description = description;
            this.values = values;
        }

        public StartGame(string description, int[,] values)
        {
            this.Description = description;
            this.values = values;
        }
    }
}
