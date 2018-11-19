namespace Game2048.Tests
{
    using App.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Implementations;
    using Services.Models;

    [TestClass]
    public class MoveKeyTest
    {
        private IGameService games;
        private Game game;
        private GameGridServiceModel serviceGame;

        [TestInitialize]
        public void SetInit()
        {
            this.games = new GameService();
            this.game = new Game
            {
                Field = new int[,]
                {
                    {4,0,0,0},
                    {2,0,0,0},
                    {8,0,0,0},
                    {16,32,0,0}
                }
            };
            this.serviceGame = new GameGridServiceModel()
            {
                Field = this.game.Field,
                CurrentScore = this.game.CurrentScore
            };
        }

        [TestMethod]
        public void MoveKeyCommandReturnsFalseIfMatrixNotMoved()
        {
            Assert.AreEqual(false, this.games.MoveKey("37", this.serviceGame));
            Assert.AreEqual(false, this.games.MoveKey("40", this.serviceGame));
        }

        [TestMethod]
        public void MoveKeyCommandReturnsTrueIfMatrixMoves()
        {
            Assert.AreEqual(true, this.games.MoveKey("39", this.serviceGame));
            Assert.AreEqual(true, this.games.MoveKey("38", this.serviceGame));
        }
    }
}
