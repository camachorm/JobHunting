#region Imports

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NAC.Framework;

#endregion

namespace NAC.Business
{
    public class Board : IBoardActions
    {
        #region Enums

        /// <summary>
        ///     List of possible states for the board squares
        /// </summary>
        public enum SquareState
        {
            Unchecked,
            Noughts,
            Crosses
        }

        #endregion

        #region Constructors

        public Board()
        {
            // Avoid duplicating initialization statement for easier readibility
            _availableSquares = _allSquares.ToArray().ToList();
        }

        #endregion

        /// <summary>
        ///     Changes the board according to the requested move by the requested player
        /// </summary>
        /// <param name="player">The player requesting the move</param>
        /// <param name="coordinates">The choice the player made</param>
        /// <exception cref="InvalidMoveException">Invalid coordinates to make a move</exception>
        void IBoardActions.MakeAMove(IPlayer player, Point coordinates)
        {
            // TODO: Validate X and Y consistency
            if (!((IBoardActions) this).IsValidMove(coordinates))
                throw new InvalidMoveException(string.Format("The square is unavailalble, current state is [{0}]",
                    _boardSquares[coordinates.Y, coordinates.X]));

            // Performs the move
            _boardSquares[coordinates.Y, coordinates.X] = player.SquareState;

            // Updates internal tracker of available Squares
            _availableSquares.Remove(coordinates);

            // Increases the counter
            TotalMovesMade++;
        }

        /// <summary>
        ///     Provides a centralized way of obtaining a Random Choice that allows for individual testing of functionality
        /// </summary>
        /// <returns>One of the available Squares in the board, throws an exception if none are present</returns>
        /// <exception cref="ArgumentOutOfRangeException">If no points are available</exception>
        Point IBoardActions.GetRandomChoice()
        {
            return AvailableSquares.ElementAt(new Random().Next(0, AvailableSquares.Count()));
        }

        /// <summary>
        ///     Allows for external validation of an intended move
        /// </summary>
        /// <param name="coordinates">The coordinates to check if they are available</param>
        /// <returns>True or false</returns>
        bool IBoardActions.IsValidMove(Point coordinates)
        {
            return _availableSquares.Contains(coordinates);
        }

        #region Fields

        private readonly SquareState[,] _boardSquares = new SquareState[3, 3];

        private readonly List<Point> _allSquares = new List<Point>
        {
            new Point(0, 0),
            new Point(0, 1),
            new Point(0, 2),
            new Point(1, 0),
            new Point(1, 1),
            new Point(1, 2),
            new Point(2, 0),
            new Point(2, 1),
            new Point(2, 2)
        };

        private readonly List<Point> _availableSquares;

        #endregion

        #region Properties

        /// <summary>
        ///     Currently available squares on the board
        /// </summary>
        public IEnumerable<Point> AvailableSquares => _availableSquares;

        /// <summary>
        ///     Squares selected by Noughts Player
        /// </summary>
        public IEnumerable<Point> NoughtsSquares
        {
            get
            {
                return _allSquares.Where(
                    coordinates =>
                        !_availableSquares.Contains(coordinates) &&
                        _boardSquares[coordinates.Y, coordinates.X] == SquareState.Noughts);
            }
        }

        public int TotalMovesMade { get; private set; }

        public SquareState GetSquare(Point coordinates)
        {
            return _boardSquares[coordinates.Y, coordinates.X];
        }

        public SquareState GetSquare(int y, int x)
        {
            return GetSquare(new Point(x, y));
        }

        /// <summary>
        ///     Squares selected by Crosses Player
        /// </summary>
        public IEnumerable<Point> CrossesSquares
        {
            get
            {
                return _allSquares.Where(
                    coordinates =>
                        !_availableSquares.Contains(coordinates) &&
                        _boardSquares[coordinates.Y, coordinates.X] == SquareState.Crosses);
            }
        }

        #endregion
    }
}