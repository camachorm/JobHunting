#region Imports

using NAC.Business;
using NAC.Framework;
using NUnit.Framework;

#endregion

namespace NAC.Tests.Unit
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void InvalidPlayerType_Test()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<InvalidPlayerException>(() => new Player("Unchecked", Board.SquareState.Unchecked),
                "Using an invalid player type should throw an InvalidPlayerException");
        }
    }
}