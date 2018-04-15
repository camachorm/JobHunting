#region Imports

using System.Diagnostics.CodeAnalysis;
using NAC.Framework;
using NAC.UI.Framework.Utils;

#endregion

namespace NAC.Business
{
    public class Player : IPlayer
    {
        public Player(string name, Board.SquareState squareState)
        {
            Name = name;
            if (squareState != Board.SquareState.Noughts && squareState != Board.SquareState.Crosses)
                throw new InvalidPlayerException("A player must represent, either Noughts or Crosses");
            SquareState = squareState;
        }

        [ExcludeFromCodeCoverage]
        public string Name { get; }

        [ExcludeFromCodeCoverage]
        public Board.SquareState SquareState { get; }

        [ExcludeFromCodeCoverage]
        public override string ToString()
        {
            return string.Format("[{0}({1})]", Name, SquareState.ToChar());
        }
    }
}