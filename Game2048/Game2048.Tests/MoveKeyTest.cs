namespace Game2048.Tests
{
    using App.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Implementations;

    [TestClass]
    public class MoveKeyTest
    {
        private IGameService games;
        private GameGrid game;

        [TestInitialize]
        public void SetInit()
        {
            this.games = new GameService();
            this.game = new GameGrid
            {
                Field = new int[,]
                {
                    {4,0,0,0},
                    {2,0,0,0},
                    {8,0,0,0},
                    {16,32,0,0}
                }
            };
        }

        [TestMethod]
        public void MoveKeyCommandReturnsFalseIfMatrixNotMoved()
        {
            Assert.AreEqual(false, this.games.MoveKey("37", this.game.Field));
            Assert.AreEqual(false, this.games.MoveKey("40", this.game.Field));
        }

        [TestMethod]
        public void MoveKeyCommandReturnsTrueIfMatrixMoves()
        {
            Assert.AreEqual(true, this.games.MoveKey("39", this.game.Field));
            Assert.AreEqual(true, this.games.MoveKey("38", this.game.Field));
        }
    }
}
