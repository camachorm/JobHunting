using NAC.Business;

namespace NAC.UI.Framework
{
    public interface IGameModel
    {
        IPlayerModel Crosses { get; }
        IPlayerModel Noughts { get; }
        IPlayerModel CurrentPlayer { get; }

        IBoardModel Board { get; }

        GameResults GameResult { get; }

        int TotalGames { get; }
        bool IsOver { get; }

        void MakeAMove();
        void PlayAGame();
        void StartNewGame();
    }
}