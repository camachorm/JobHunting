#region Imports

using NAC.Business;
using NAC.UI.Framework;
using NAC.UI.Framework.Utils;

#endregion

namespace NAC.UI.Model
{
    /// <summary>
    ///     Model class that wraps the business model and extends it with any view-specific required information as needed
    /// </summary>
    public class GameModel : IGameModel
    {
        private readonly IGame _game;

        public GameModel(IGame game)
        {
            _game = game;
            _game.MovePerformed += (sender, args) => this.AsConsoleView().Render();
        }

        public GameModel() : this(new Game())
        {
        }

        public IPlayerModel Crosses => new PlayerModel(_game.Crosses);

        public IPlayerModel Noughts => new PlayerModel(_game.Noughts);

        public IPlayerModel CurrentPlayer => new PlayerModel(_game.CurrentPlayer);

        public GameResults GameResult => _game.GameResult;

        public int TotalGames => _game.TotalGames;

        public IBoardModel Board => new BoardModel(_game.GameBoard);

        public bool IsOver => _game.IsOver;

        public IGame Game => _game;

        public void MakeAMove()
        {
            _game.MakeAMove();
        }

        public void PlayAGame()
        {
            _game.PlayAGame(1000);
        }

        public void StartNewGame()
        {
            this.AsConsoleView().Render();
            _game.StartNewGame();
        }

        public void PlayAGame(int sleepInterval)
        {
            _game.PlayAGame(sleepInterval);
        }
    }
}