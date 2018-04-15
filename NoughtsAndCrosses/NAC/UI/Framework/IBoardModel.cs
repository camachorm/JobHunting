#region Imports

using System.Collections.Generic;
using System.Drawing;
using NAC.Business;

#endregion

namespace NAC.UI.Framework
{
    public interface IBoardModel
    {
        IEnumerable<Point> AvailableSquares { get; }

        IEnumerable<Point> CrossesSquares { get; }

        IEnumerable<Point> KnotsSquares { get; }

        /// <summary>
        ///     Count of the total moves made in the board
        /// </summary>
        int TotalMovesMade { get; }

        Board.SquareState GetSquare(int y, int x);
    }
}