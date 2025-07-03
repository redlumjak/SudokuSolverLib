// See https://aka.ms/new-console-template for more information
using System.Text.Json;

using System;
using System.Text.Json.Serialization;
using SudokuSolver;

namespace SudokuSolver
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Please pass a json file with the sudoku to be solved");
                    throw new Exception("no json file");
                }

                // 1. create board from json file passed in
                Console.WriteLine("Opening file" + args[0]);
                string json = File.ReadAllText(args[0]);
                Console.WriteLine();
                Console.WriteLine("Initial Board:");
                Console.WriteLine(json);
                var board = Board.FromJson(json);

                // 2. init all possible notes to be 1-9
                board.setAllNotes();

                // 3. keep running through strategies until it's solved. 
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception " + e.ToString());
            }
        }
    }
}

