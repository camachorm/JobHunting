#region Imports

using System;
using System.Drawing;

#endregion

namespace NAC.Business
{
    public interface IGame
    {
        IPlayer Crosses { get; }
        IPlayer Noughts { get; }
        IPlayer CurrentPlayer { get; }
        IBoard GameBoard { get; }
        GameResults GameResult { get; }

        event EventHandler MovePerformed;

        int TotalGames { get; }

        bool IsOver { get; }
        bool IsNoughtsWinner { get; }
        bool IsCrossesWinner { get; }

        void StartNewGame();
        Point GetRandomChoice();
        void MakeAMove();
        void PlayAGame(int sleepInterval);
    }
}