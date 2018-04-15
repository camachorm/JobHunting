#region Imports

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using NAC.Framework;

#endregion

namespace NAC.Business
{
    public class Game : IGameActions
    {
        private IBoardActions _gameBoard;
        private IPlayer _lastMoveBy;

        public event EventHandler MovePerformed;

        #region Constructors

        /// <summary>
        ///     Public constructor
        /// </summary>
        public Game()
            : this(new Player("Noughts", Board.SquareState.Noughts), new Player("Crosses", Board.SquareState.Crosses))
        {
        }

        /// <summary>
        ///     Public constructor
        /// </summary>
        /// <param name="noughts">The player that will be taking the Noughts</param>
        /// <param name="crosses">The player that will be taking the Crosses</param>
        public Game(IPlayer noughts, IPlayer crosses)
        {
            Noughts = noughts;
            Crosses = crosses;
            StartNewGame();
        }

        #endregion

        #region Properties

        public IPlayer Noughts { get; }
        public IPlayer Crosses { get; }

        /// <summary>
        ///     Returns the CurrentPlayer taking a turn, defaulting to Crosses as a startup player
        /// </summary>
        public IPlayer CurrentPlayer => _lastMoveBy == Crosses ? Noughts : Crosses;

        public int TotalGames { get; private set; }

        public IBoard GameBoard => _gameBoard;

        public bool IsOver
            =>
                IsWinner(GameBoard.NoughtsSquares) || IsWinner(GameBoard.CrossesSquares) ||
                !GameBoard.AvailableSquares.Any();

        public bool IsNoughtsWinner => IsWinner(GameBoard.NoughtsSquares);
        public bool IsCrossesWinner => IsWinner(GameBoard.CrossesSquares);

        public GameResults GameResult
        {
            get
            {
                if (IsNoughtsWinner)
                {
                    return GameResults.Noughts;
                }
                if (IsCrossesWinner)
                {
                    return GameResults.Crosses;
                }

                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (!GameBoard.AvailableSquares.Any())
                    return GameResults.Draw;

                return GameResults.Unfinished;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Validates if a list of Points contains a winning combination
        /// </summary>
        /// <param name="pointsList">The list of points to validate</param>
        /// <returns>True if a winning combination was found, false otherwise</returns>
        private bool IsWinner(IEnumerable<Point> pointsList)
        {
            var points = pointsList as Point[] ?? pointsList.ToArray();
            return (points.Contains(new Point(0, 0)) &&
                    points.Contains(new Point(0, 1)) &&
                    points.Contains(new Point(0, 2))) ||
                   (points.Contains(new Point(0, 0)) &&
                    points.Contains(new Point(1, 0)) &&
                    points.Contains(new Point(2, 0))) ||
                   (points.Contains(new Point(1, 0)) &&
                    points.Contains(new Point(1, 1)) &&
                    points.Contains(new Point(1, 2))) ||
                   (points.Contains(new Point(2, 0)) &&
                    points.Contains(new Point(2, 1)) &&
                    points.Contains(new Point(2, 2))) ||
                   (points.Contains(new Point(2, 0)) &&
                    points.Contains(new Point(1, 1)) &&
                    points.Contains(new Point(0, 2))) ||
                   (points.Contains(new Point(0, 0)) &&
                    points.Contains(new Point(1, 1)) &&
                    points.Contains(new Point(2, 2)));
        }

        /// <summary>
        ///     Perform a move for a specified player, since this is not a publicly known method, it bypasses rule enforcing
        ///     (Allows same player to do subsequent moves aka: GOD mode)
        /// </summary>
        /// <param name="player">The player performing the move</param>
        /// <param name="coordinates">The coordinates the player has chosen</param>
        void IGameActions.MakeAMove(IPlayer player, Point coordinates)
        {
            _gameBoard.MakeAMove(player, coordinates);

            _lastMoveBy = player;
        }

        public void StartNewGame()
        {
            _gameBoard = new Board();
            TotalGames++;
        }

        public Point GetRandomChoice()
        {
            return _gameBoard.GetRandomChoice();
        }

        /// <summary>
        ///     Shorthand version of MakeAMove for ease of use
        /// </summary>
        public void MakeAMove()
        {
            if (IsOver)
                throw new GameOverException(
                    "You cannot make a move when the game has already finished. Call StartNewGame() first!");
            ((IGameActions) this).MakeAMove(CurrentPlayer, GetRandomChoice());
        }

        public void PlayAGame(int sleepInterval)
        {
            while (!IsOver)
            {
                // Let current user make a move
                MakeAMove();

                Thread.Sleep(sleepInterval);

                // Update printout
                MovePerformed?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
    }

    public enum GameResults
    {
        Noughts,
        Crosses,
        Draw,
        Unfinished
    }
}