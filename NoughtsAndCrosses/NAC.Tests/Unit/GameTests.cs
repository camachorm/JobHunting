#region Imports

using System.Drawing;
using System.Linq;
using NAC.Business;
using NAC.Framework;
using NUnit.Framework;

#endregion

namespace NAC.Tests.Unit
{
    [TestFixture]
    internal class GameTests
    {
        private IGame _game;

        [Test]
        public void GameResult_ReturnsCorrectValue_Test()
        {
            // Reuse above code to initialize to proper state
            var game = new Game() as IGameActions;
            game.MakeAMove(game.Noughts, new Point(0, 0));

            game.MakeAMove(game.Crosses, new Point(2, 0));

            Assert.IsTrue(game.GameResult == GameResults.Unfinished,
                "The result should be unfinished as the game is not yet over");

            game.MakeAMove(game.Noughts, new Point(0, 1));

            game.MakeAMove(game.Crosses, new Point(2, 1));

            game.MakeAMove(game.Noughts, new Point(0, 2));

            // Noughts wins
            Assert.IsTrue(game.IsOver, "Game should be over");
            Assert.IsTrue(game.IsNoughtsWinner, "Noughts should be the winner");
            Assert.IsTrue(game.GameResult == GameResults.Noughts, "Noughts should be the winner");

            game.StartNewGame();
            game.MakeAMove(game.Crosses, new Point(0, 0));

            game.MakeAMove(game.Noughts, new Point(2, 0));

            game.MakeAMove(game.Crosses, new Point(0, 1));

            game.MakeAMove(game.Noughts, new Point(2, 1));

            game.MakeAMove(game.Crosses, new Point(0, 2));

            // Crosses wins
            Assert.IsTrue(game.IsOver, "Game should be over");
            Assert.IsTrue(game.IsCrossesWinner, "Crosses should be the winner");
            Assert.IsTrue(game.GameResult == GameResults.Crosses, "Crosses should be the winner");

            game.StartNewGame();
            game.MakeAMove(game.Crosses, new Point(0, 0));
            game.MakeAMove(game.Crosses, new Point(0, 2));
            game.MakeAMove(game.Crosses, new Point(1, 1));
            game.MakeAMove(game.Crosses, new Point(2, 1));

            game.MakeAMove(game.Noughts, new Point(0, 1));
            game.MakeAMove(game.Noughts, new Point(1, 0));
            game.MakeAMove(game.Noughts, new Point(1, 2));
            game.MakeAMove(game.Noughts, new Point(2, 0));
            game.MakeAMove(game.Noughts, new Point(2, 2));

            // Draw
            Assert.IsTrue(game.IsOver, "Game should be over");
            Assert.IsFalse(game.IsCrossesWinner, "Crosses should not be the winner");
            Assert.IsFalse(game.IsNoughtsWinner, "Noughts should not be the winner");
            Assert.IsTrue(game.GameResult == GameResults.Draw, "The result should be a draw");

            game.StartNewGame();
            game.MakeAMove(game.Crosses, new Point(0, 0));
            game.MakeAMove(game.Crosses, new Point(0, 1));
            game.MakeAMove(game.Crosses, new Point(0, 2));
            Assert.IsTrue(game.IsOver, "Game should be over");
            Assert.IsTrue(game.IsCrossesWinner, "Crosses should not be the winner");

            // TODO: Test all other possible win combinations
        }

        [Test]
        public void MakeAMove_GameOver_Test()
        {
            // Reuse above code to initialize to proper state
            PlayAGame_Success_Test();

            Assert.Throws<GameOverException>(() => _game.MakeAMove(),
                "Making a move after the game is over should have thrown an exception");
        }

        [Test]
        public void MakeAMove_Success_Test()
        {
            _game = new Game();
            Assert.DoesNotThrow(() => _game.MakeAMove(), "Making a valid move should not throw any exceptions");
            Assert.DoesNotThrow(() => _game.MakeAMove(), "Making a valid move should not throw any exceptions");
            Assert.DoesNotThrow(() => _game.MakeAMove(), "Making a valid move should not throw any exceptions");
        }

        [Test]
        public void PlayAGame_Success_Test()
        {
            _game = new Game();
            Assert.DoesNotThrow(() => _game.PlayAGame(0), "Playing a whole game should not throw any exceptions");
            Assert.IsTrue(_game.IsOver, "Game should be over");
        }

        [Test]
        public void StartANewGame_IncrementsCounter_Test()
        {
            // Reuse above code to initialize to proper state
            PlayAGame_Success_Test();
            _game.StartNewGame();
            Assert.IsTrue(_game.TotalGames == 2, "Starting a new game failed to increment the counter");
            Assert.IsTrue(_game.GameBoard.AvailableSquares.Count() == 9,
                "Starting a new game failed reset the number of available squares");
        }
    }
}