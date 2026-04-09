using System;
using System.Collections.Generics;
using System.IO;
using System.Linq;

namespace Minesweeper.Core
{
	public static class HighScoreManager
	{
		private static string filepath = Path.Combine("..", "..", "data", "highscore.csv");

		public static List<HighScore> LoadScores()
		{
			var scores = new List<HighScore>();
            if (!File.Exists(filePath)) return scores;

            foreach (var line in File.ReadAllLines(filePath).Skip(1)) // skip header
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                try { scores.Add(HighScore.FromCsv(line)); } catch { }
            }

            return scores;
        }

        public static void SaveScore(HighScore score)
        {
            var scores = LoadScores();
            scores.Add(score);

            var topScores = scores
                .Where(s => s.Size == score.Size)
                .OrderBy(s => s.Seconds)
                .ThenBy(s => s.Moves)
                .Take(5)
                .ToList();

            // Rewrite file
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("size,seconds,moves,seed,timestamp");
                foreach (var s in topScores)
                    writer.WriteLine(s.ToString());
            }
        }

        public static void PrintTopScores(int size)
        {
            var scores = LoadScores()
                .Where(s => s.Size == size)
                .OrderBy(s => s.Seconds)
                .ThenBy(s => s.Moves)
                .Take(5);

            Console.WriteLine("Top 5 High Scores:");
            foreach (var s in scores)
                Console.WriteLine($"Time: {s.Seconds}s, Moves: {s.Moves}, Seed: {s.Seed}");
        }
    }
}
