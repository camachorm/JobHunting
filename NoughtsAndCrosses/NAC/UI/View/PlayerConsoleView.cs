#region Imports

using System;
using NAC.UI.Framework;

#endregion

namespace NAC.UI.View
{
    public class PlayerConsoleView : IView
    {
        private readonly IPlayerModel _player;

        public PlayerConsoleView(IPlayerModel player)
        {
            _player = player;
        }

        public void Render()
        {
            Console.Write(_player.ToString());
        }
    }
}