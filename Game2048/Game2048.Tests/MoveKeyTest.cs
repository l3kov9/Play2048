namespace Game2048.Tests
{
    using App.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Implementations;
    using Services.Models;

    using static Common.Directions;

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
            Assert.AreEqual(false, this.games.MoveKey(this.serviceGame, left.ToString()));
            Assert.AreEqual(false, this.games.MoveKey(this.serviceGame, down.ToString()));
            Assert.AreEqual(32, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void MoveKeyCommandReturnsTrueIfMatrixMoves()
        {
            Assert.AreEqual(true, this.games.MoveKey(this.serviceGame, right.ToString()));
            Assert.AreEqual(true, this.games.MoveKey(this.serviceGame, up.ToString()));
            Assert.AreEqual(32, this.serviceGame.MaxNumber);
        }
    }
}