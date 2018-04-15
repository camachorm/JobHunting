#region Imports

using System;
using NAC.UI.Framework;
using NAC.UI.Framework.Utils;

#endregion

namespace NAC.UI.View
{
    public class BoardConsoleView : IView
    {
        private readonly IBoardModel _board;

        public BoardConsoleView(IBoardModel board)
        {
            _board = board;
        }

        /// <summary>
        ///     Renders the Board onto a specific media
        /// </summary>
        public void Render()
        {
            // Place cursor at start Location
            Console.SetCursorPosition(3, 4);
            Console.Write("[{0}][{1}][{2}]", _board.GetSquare(0, 0).ToChar(), _board.GetSquare(0, 1).ToChar(), _board.GetSquare(0, 2).ToChar());
            Console.SetCursorPosition(3, 5);
            Console.Write("[{0}][{1}][{2}]", _board.GetSquare(1, 0).ToChar(), _board.GetSquare(1, 1).ToChar(), _board.GetSquare(1, 2).ToChar());
            Console.SetCursorPosition(3, 6);
            Console.Write("[{0}][{1}][{2}]", _board.GetSquare(2, 0).ToChar(), _board.GetSquare(2, 1).ToChar(), _board.GetSquare(2, 2).ToChar());
        }
    }
}