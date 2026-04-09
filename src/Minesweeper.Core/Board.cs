using System;

namespace Minesweeper.Core
{
    public class Board
    {
        private Tile[,] tiles;
        private int rows;
        private int cols;
        private int mineCount;
        private int seed;

        public Board(int rows, int cols, int mineCount)
            : this(rows, cols, mineCount, Environment.TickCount) // default seed
        {
        }
public Tile GetTile(int row, int col)
{
    return tiles[row, col];
}
        public Board(int rows, int cols, int mineCount, int seed)
        {
            this.rows = rows;
            this.cols = cols;
            this.mineCount = mineCount;
            this.seed = seed;

            tiles = new Tile[rows, cols];

            // Initialize all tiles
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    tiles[r, c] = new Tile();
                }
            }

            PlaceMines();
            CalculateAdjacentMines();
        }

        private void PlaceMines()
        {
            Random rand = new Random(seed);
            int placed = 0;
            while (placed < mineCount)
            {
                int r = rand.Next(rows);
                int c = rand.Next(cols);
                if (!tiles[r, c].IsMine)
                {
                    tiles[r, c].IsMine = true;
                    placed++;
                }
            }
        }

        private void CalculateAdjacentMines()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (tiles[r, c].IsMine)
                    {
                        tiles[r, c].AdjacentMines = -1;
                        continue;
                    }

                    int count = 0;
                    for (int dr = -1; dr <= 1; dr++)
                    {
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            int nr = r + dr;
                            int nc = c + dc;
                            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                            {
                                if (tiles[nr, nc].IsMine) count++;
                            }
                        }
                    }
                    tiles[r, c].AdjacentMines = count;
                }
            }
        }

        public void RevealTile(int row, int col)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
                return;

            Tile tile = tiles[row, col];
            if (tile.IsRevealed || tile.IsFlagged)
                return;

            tile.IsRevealed = true;

            if (tile.AdjacentMines == 0 && !tile.IsMine)
            {
                for (int r = row - 1; r <= row + 1; r++)
                {
                    for (int c = col - 1; c <= col + 1; c++)
                    {
                        if (r == row && c == col) continue;
                        RevealTile(r, c);
                    }
                }
            }
        }

        public void ToggleFlag(int row, int col)
        {
            Tile tile = tiles[row, col];
            if (!tile.IsRevealed)
                tile.IsFlagged = !tile.IsFlagged;
        }

        public bool CheckWin()
        {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (!tiles[r, c].IsMine && !tiles[r, c].IsRevealed)
                        return false;
            return true;
        }

        public bool CheckLose()
        {
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (tiles[r, c].IsMine && tiles[r, c].IsRevealed)
                        return true;
            return false;
        }

        public void PrintBoard(bool revealMines = false)
        {
            Console.Write("  ");
            for (int c = 0; c < cols; c++) Console.Write(c + " ");
            Console.WriteLine();

            for (int r = 0; r < rows; r++)
            {
                Console.Write(r + " ");
                for (int c = 0; c < cols; c++)
                {
                    Tile t = tiles[r, c];
                    if (t.IsRevealed)
                    {
                        if (t.IsMine) Console.Write("b ");
                        else if (t.AdjacentMines == 0) Console.Write(". ");
                        else Console.Write(t.AdjacentMines + " ");
                    }
                    else if (t.IsFlagged) Console.Write("f ");
                    else if (revealMines && t.IsMine) Console.Write("b ");
                    else Console.Write("# ");
                }
                Console.WriteLine();
            }
        }
    }
}
