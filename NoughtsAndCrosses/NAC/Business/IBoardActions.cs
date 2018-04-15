#region Imports

using System.Drawing;

#endregion

namespace NAC.Business
{
    public interface IBoardActions : IBoard
    {
        void MakeAMove(IPlayer player, Point choice);

        Point GetRandomChoice();

        bool IsValidMove(Point coordinates);
    }
}