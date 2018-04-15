#region Imports

using System;
using NAC.Business;
using NAC.UI.View;

#endregion

namespace NAC.UI.Framework.Utils
{
    public static class GameModelExtensions
    {
        public static GameConsoleView AsConsoleView(this IGameModel model)
        {
            return new GameConsoleView(model);
        }

        public static char ToChar(this Board.SquareState square)
        {
            switch (square)
            {
                case Board.SquareState.Unchecked:
                    return '_';
                case Board.SquareState.Noughts:
                    return 'O';
                case Board.SquareState.Crosses:
                    return 'X';
                default:
                    throw new ArgumentOutOfRangeException(nameof(square), square, null);
            }
        }
        
    }
}