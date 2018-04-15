#region Imports

using System;
using System.Linq;
using NAC.Business;
using NAC.Framework;
using NUnit.Framework;

#endregion

namespace NAC.Tests.Unit
{
    [TestFixture]
    public class BoardTests
    {
        private readonly IPlayer _noughts = new Player("Noughts", Board.SquareState.Noughts);
        private readonly IPlayer _crosses = new Player("Crosses", Board.SquareState.Crosses);
        private readonly IPlayer _invalid = new Player("Unknown Player", Board.SquareState.Crosses);
        private IBoardActions _board;

        [Test]
        public void CrossesSquares_UpdatesAfterMoves_Test()
        {
            _board = new Board();
            var startCounter = _board.TotalMovesMade;
            _board.MakeAMove(_crosses, _board.GetRandomChoice());
            Assert.IsTrue(_board.CrossesSquares.Count() == ++startCounter,
                "Crosses moves made didnt increment as expected");
        }

        [Test]
        public void GetSquare_AfterUpdateCorrectResult_Test()
        {
            _board = new Board();
            var choice = _board.GetRandomChoice();
            _board.MakeAMove(_noughts, choice);
            Assert.IsTrue(_board.GetSquare(choice) == Board.SquareState.Noughts,
                "Correctly obtaining the square after update");
        }


        [Test]
        public void GetSquare_CorrectResult_Test()
        {
            _board = new Board();
            Assert.IsTrue(_board.GetSquare(0, 0) == Board.SquareState.Unchecked,
                "Correctly obtaining the square after update");
        }


        [Test]
        public void GetSquare_OutOfBounds_Test()
        {
            _board = new Board();
            Assert.Throws<IndexOutOfRangeException>(() => _board.GetSquare(4, 4),
                "Trying to get a square out of bounds didnt throw an exception?");
        }


        [Test]
        public void KnotsSquares_UpdatesAfterMoves_Test()
        {
            _board = new Board();
            var startCounter = _board.TotalMovesMade;
            _board.MakeAMove(_noughts, _board.GetRandomChoice());
            Assert.IsTrue(_board.NoughtsSquares.Count() == ++startCounter,
                "Noughts moves made didnt increment as expected");
        }

        [Test]
        public void MakeAMove_IncrementsTotalMoves_Test()
        {
            _board = new Board();
            var startCounter = _board.TotalMovesMade;
            _board.MakeAMove(_noughts, _board.GetRandomChoice());
            Assert.IsTrue(_board.TotalMovesMade == ++startCounter, "Total moves made didnt increment as expected");
        }

        [Test]
        public void MakeAMove_InvalidMove_Test()
        {
            _board = new Board();
            var choice = _board.GetRandomChoice();
            _board.MakeAMove(_noughts, choice);
            Assert.Throws<InvalidMoveException>(() => _board.MakeAMove(_noughts, choice),
                "Performing the same move twice should have thrown an InvalidMoveException");
        }
    }
}