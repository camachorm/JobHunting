using System.Drawing;

namespace NAC.Business
{
    public interface IGameActions : IGame
    {
        void MakeAMove(IPlayer player, Point coordinates);
    }
}