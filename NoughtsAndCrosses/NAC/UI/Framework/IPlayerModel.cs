#region Imports

using NAC.Business;

#endregion

namespace NAC.UI.Framework
{
    public interface IPlayerModel
    {
        string Name { get; }
        Board.SquareState SquareState { get; }
        IPlayer Player { get; }
        string ToString();
    }
}