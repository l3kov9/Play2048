namespace Game2048.Tests
{
    using App.Models;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Implementations;
    using System.Linq;

    [TestClass]
    public class StartGameTest
    {
        private readonly IGameService games;

        public StartGameTest()
        {
            this.games = new GameService();
        }

        [TestMethod]
        public void FieldGetsCorrectValues()
        {
            var game = new Game();

            for (int i = 0; i < 1000; i++)
            {
                game.Field = games.RestartGameField();
                var nonZeroNumbers = GameFieldHelpers.GetNonZeroNumbers(game.Field);

                Assert.AreEqual(2, nonZeroNumbers.Count);
                Assert.AreEqual(2, nonZeroNumbers.Count(n => n == 2 || n == 4));
                Assert.IsTrue(nonZeroNumbers.Sum() == 4 || nonZeroNumbers.Sum() == 6);
            }
        }
    }
}
