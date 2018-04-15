#region Imports

using System.Collections.Generic;
using System.Drawing;

#endregion

namespace NAC.Business
{
    public interface IBoard
    {
        IEnumerable<Point> AvailableSquares { get; }

        IEnumerable<Point> CrossesSquares { get; }

        IEnumerable<Point> NoughtsSquares { get; }

        /// <summary>
        ///     Count of the total moves made in the board
        /// </summary>
        int TotalMovesMade { get; }

        Board.SquareState GetSquare(Point coordinates);

        Board.SquareState GetSquare(int y, int x);
    }
}