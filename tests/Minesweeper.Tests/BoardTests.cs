using Minesweeper.Core;
using System;
using Xunit;

namespace Minesweeper.Tests
{
    public class BoardTests
    {
        [Fact]
        public void TestBoardInitialization()
        {
            var board = new Board(8, 8, 10);
            int unrevealed = 0;

            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                    if (!board.GetTile(r, c).IsRevealed) unrevealed++;

            Assert.Equal(64, unrevealed); // All tiles start unrevealed
        }

        [Fact]
        public void TestRevealZeroCascades()
        {
            var board = new Board(3, 3, 0); // no mines
            board.RevealTile(1, 1);

            int revealedCount = 0;
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    if (board.GetTile(r, c).IsRevealed) revealedCount++;

            Assert.Equal(9, revealedCount); // all tiles should reveal
        }

        [Fact]
        public void TestFlagging()
        {
            var board = new Board(3, 3, 1);
            board.ToggleFlag(0, 0);

            Assert.True(board.GetTile(0, 0).IsFlagged);
            board.ToggleFlag(0, 0);
            Assert.False(board.GetTile(0, 0).IsFlagged);
        }

        [Fact]
        public void TestWinCondition()
        {
            var board = new Board(2, 2, 1);

            // Manually reveal all non-mine tiles
            for (int r = 0; r < 2; r++)
                for (int c = 0; c < 2; c++)
                    if (!board.GetTile(r, c).IsMine)
                        board.RevealTile(r, c);

            Assert.True(board.CheckWin());
        }

        [Fact]
        public void TestLoseCondition()
        {
            var board = new Board(2, 2, 1);

            // Reveal a mine
            for (int r = 0; r < 2; r++)
            {
                for (int c = 0; c < 2; c++)
                {
                    if (board.GetTile(r, c).IsMine)
                    {
                        board.RevealTile(r, c);
                        Assert.True(board.CheckLose());
                        return;
                    }
                }
            }
        }
    }
}
