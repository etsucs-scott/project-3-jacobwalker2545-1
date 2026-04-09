using System;

namespace Minesweeper.Core
{
    public class Tile
    {
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int AdjacentMines { get; set; }

        // Default constructor
        public Tile()
        {
            IsMine = false;
            IsRevealed = false;
            IsFlagged = false;
            AdjacentMines = 0;
        }

        // Constructor with mine flag
        public Tile(bool isMine)
        {
            IsMine = isMine;
            IsRevealed = false;
            IsFlagged = false;
            AdjacentMines = 0;
        }
    }
}
