#region Imports

using NAC.Business;
using NAC.UI.Framework;

#endregion

namespace NAC.UI.Model
{
    public class PlayerModel : IPlayerModel
    {
        private readonly IPlayer _player;

        public PlayerModel(IPlayer player)
        {
            _player = player;
        }

        public string Name => _player.Name;
        public Board.SquareState SquareState => _player.SquareState;

        public IPlayer Player => _player;

        public override string ToString()
        {
            return _player.ToString();
        }
      
    }
}