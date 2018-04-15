#region Imports

using System;
using NAC.UI.Framework;
using NAC.UI.Model;

#endregion

namespace NAC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Prompt user to start the game
            Console.WriteLine("Press any key to start tic tack toe");
            Console.ReadKey();

            IGameModel game = new GameModel();
            while (true)
            {
                try
                {
                    game.PlayAGame();

                    // Check if user wants to play another game
                    Console.WriteLine("Would you like to play again? (Press the 'N' key if not)");

                    var keyInfo = Console.ReadKey();

                    if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N') break;
                    game.StartNewGame();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + e.StackTrace);
                    break;
                }
            }
        }
    }
}