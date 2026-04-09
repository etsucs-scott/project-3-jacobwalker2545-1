using System;
using Minesweeper.Core;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Minesweeper Menu:");
            Console.WriteLine("1) 8x8");
            Console.WriteLine("2) 12x12");
            Console.WriteLine("3) 16x16");
            Console.WriteLine("q) Quit");
            Console.Write("Choose board size: ");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "q") break;

            int size = choice switch
            {
                "1" => 8,
                "2" => 12,
                "3" => 16,
                _ => 0
            };

            if (size == 0)
            {
                Console.WriteLine("Invalid choice. Press Enter to retry.");
                Console.ReadLine();
                continue;
            }

            int mines = size switch
            {
                8 => 10,
                12 => 25,
                16 => 40,
                _ => 0
            };

            Console.Write("Enter seed (leave blank = random): ");
            string seedInput = Console.ReadLine();
            int seed = string.IsNullOrEmpty(seedInput) ? new Random().Next() : int.Parse(seedInput);
            Console.WriteLine($"Using seed: {seed}");

            Board board = new Board(size, size, mines, seed);

            while (true)
            {
                Console.Clear();
                board.PrintBoard();
                Console.WriteLine("Commands: r row col | f row col | q");
                Console.Write("> ");
                string input = Console.ReadLine();
                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;

                string command = parts[0].ToLower();
                if (command == "q") break;

                if (parts.Length != 3)
                {
                    Console.WriteLine("Invalid command. Press Enter.");
                    Console.ReadLine();
                    continue;
                }

                if (!int.TryParse(parts[1], out int row) || !int.TryParse(parts[2], out int col))
                {
                    Console.WriteLine("Invalid coordinates. Press Enter.");
                    Console.ReadLine();
                    continue;
                }

                try
                {
                    if (command == "r") board.RevealTile(row, col);
                    else if (command == "f") board.ToggleFlag(row, col);
                    else
                    {
                        Console.WriteLine("Unknown command. Press Enter.");
                        Console.ReadLine();
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ReadLine();
                    continue;
                }

                if (board.CheckWin())
                {
                    Console.Clear();
                    board.PrintBoard();
                    Console.WriteLine("Congratulations! You won!");
                    Console.WriteLine("Press Enter to return to menu.");
                    Console.ReadLine();
                    break;
                }

                if (board.CheckLose())
                {
                    Console.Clear();
                    board.PrintBoard(true);
                    Console.WriteLine("Boom! You hit a mine. Game over.");
                    Console.WriteLine("Press Enter to return to menu.");
                    Console.ReadLine();
                    break;
                }
            }
        }
    }
}
