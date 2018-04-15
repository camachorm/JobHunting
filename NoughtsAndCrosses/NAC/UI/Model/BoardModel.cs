#region Imports

using System.Collections.Generic;
using System.Drawing;
using NAC.Business;
using NAC.UI.Framework;

#endregion

namespace NAC.UI.Model
{
    /// <summary>
    ///     Model class that wraps the business model and extends it with any view-specific required information as needed
    /// </summary>
    public class BoardModel : IBoardModel
    {
        private readonly IBoard _board;

        public BoardModel(IBoard board)
        {
            _board = board;
        }

        public IEnumerable<Point> AvailableSquares => _board.AvailableSquares;
        public IEnumerable<Point> CrossesSquares => _board.CrossesSquares;
        public IEnumerable<Point> KnotsSquares => _board.NoughtsSquares;
        public int TotalMovesMade => _board.TotalMovesMade;

        public Board.SquareState GetSquare(int y, int x)
        {
            return _board.GetSquare(y, x);
        }
    }
}