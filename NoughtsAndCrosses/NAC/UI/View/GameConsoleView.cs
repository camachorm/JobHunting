#region Imports

using System;
using NAC.Business;
using NAC.UI.Framework;

#endregion

namespace NAC.UI.View
{
    public class GameConsoleView : IView
    {
        public GameConsoleView(IGameModel game)
        {
            Game = game;
        }

        public IGameModel Game { get; }

        public void Render()
        {
            // Cleanup the console
            Console.Clear();
            
            // Ensure default colors
            Console.ResetColor();

            // Place cursor at start Location
            Console.SetCursorPosition(0, 0);
            Console.Write("Total Game Count [{0}]", Game.TotalGames);

            //Printout Crosses player
            Console.SetCursorPosition(2,1);
            if (Game.CurrentPlayer == Game.Crosses) TurnOnHighlight();
            Console.Write("Player: ");
            new PlayerConsoleView(Game.Crosses).Render();

            // Ensure default colors
            Console.ResetColor();
         
            //Printout Noughts player
            Console.SetCursorPosition(2, 2);
            if (Game.CurrentPlayer == Game.Noughts) TurnOnHighlight();
            Console.Write("Player: ");
            new PlayerConsoleView(Game.Noughts).Render();

            // Ensure default colors
            Console.ResetColor();

            //Printout Game Board
            new BoardConsoleView(Game.Board).Render();

            if (!Game.IsOver) return;

           
            Console.SetCursorPosition(1, 8);
            TurnOnHighlight();
            if (Game.GameResult == GameResults.Draw)
                Console.WriteLine("Game ended in a draw");
            else
                Console.WriteLine("Winner was {0}", Game.GameResult);


            // Ensure any following printouts go after the rendering of the Game
            // Ensure default colors
            Console.ResetColor();
            Console.WriteLine();
        }

        private void TurnOnHighlight()
        {
            // Ensure highlighting colors
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}