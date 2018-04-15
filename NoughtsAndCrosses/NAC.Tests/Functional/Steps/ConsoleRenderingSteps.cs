using System.Drawing;
using NAC.Business;
using NAC.UI.Model;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace NAC.Tests.Functional.Steps
{
    [Binding]
    public class ConsoleRenderingSteps
    {
        readonly GameModel _game = new GameModel();

        [Given(@"Crosses selects the whole First Row")]
        public void GivenCrossesSelectsTheWholeFirstRow()
        {
           ((IGameActions)_game.Game).MakeAMove(_game.Crosses.Player, new Point(0,0));
           ((IGameActions)_game.Game).MakeAMove(_game.Crosses.Player, new Point(1, 0));
           ((IGameActions)_game.Game).MakeAMove(_game.Crosses.Player, new Point(2, 0));
        }
        
        [Then(@"The Game should be over")]
        public void ThenTheGameShouldBeOver()
        {
            Assert.IsTrue(_game.IsOver);
        }
        
        [Then(@"The Crosses should be the winnner")]
        public void ThenTheCrossesShouldBeTheWinnner()
        {
            Assert.IsTrue(_game.GameResult == GameResults.Crosses, "Crosses should be the winner");
        }
    }
}
