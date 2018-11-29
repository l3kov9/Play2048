namespace Game2048.Tests
{
    using App.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Implementations;
    using Services.Models;

    using static Common.Directions;

    [TestClass]
    public class ArrowKeysTest
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
                    {2,0,2,128},
                    {8,8,8,0},
                    {8,8,2,0},
                    {16,16,16,16}
                }
            };
            this.serviceGame = new GameGridServiceModel()
            {
                Field = this.game.Field,
                CurrentScore = this.game.CurrentScore
            };
        }

        [TestMethod]
        public void RightArrowGetsCorrectValues()
        {
            this.games.MoveKey(this.serviceGame, right.ToString());
            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {0,0,4,128},
                    {0,0,8,16},
                    {0,0,16,2},
                    {0,0,32,32}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(100, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void MultipleRightArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey(this.serviceGame, right.ToString());
            }

            var actualMatrix = this.game.Field;

            var expectedMatrix = new int[,]
                {
                    {0,0,4,128},
                    {0,0,8,16},
                    {0,0,16,2},
                    {0,0,0,64}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(164, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void LeftArrowGetsCorrectValues()
        {
            this.games.MoveKey(this.serviceGame, left.ToString());

            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {4,128,0,0},
                    {16,8,0,0},
                    {16,2,0,0},
                    {32,32,0,0}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(100, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void MultipleLeftArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey(this.serviceGame, left.ToString());
            }

            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {4,128,0,0},
                    {16,8,0,0},
                    {16,2,0,0},
                    {64,0,0,0}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(164, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void UpArrowGetsCorrectValues()
        {
            this.games.MoveKey(this.serviceGame, up.ToString());

            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {2,16,2,128},
                    {16,16,8,16},
                    {16,0,2,0},
                    {0,0,16,0}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(32, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void MultipleUpArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey(this.serviceGame, up.ToString());
            }

            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {2,32,2,128},
                    {32,0,8,16},
                    {0,0,2,0},
                    {0,0,16,0}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(96, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void DownArrowGetsCorrectValues()
        {
            this.games.MoveKey(this.serviceGame, down.ToString());

            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {0,0,2,0},
                    {2,0,8,0},
                    {16,16,2,128},
                    {16,16,16,16}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(32, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void MultipleDownArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey(this.serviceGame, down.ToString());
            }

            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {0,0,2,0},
                    {0,0,8,0},
                    {2,0,2,128},
                    {32,32,16,16}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(96, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void MixedArrowKeysGetsCorrectValues()
        {
            //left
            this.games.MoveKey(this.serviceGame, left.ToString());
            //down
            this.games.MoveKey(this.serviceGame, down.ToString());
            //left
            this.games.MoveKey(this.serviceGame, left.ToString());
            //right
            this.games.MoveKey(this.serviceGame, right.ToString());

            for (int i = 0; i < 5; i++)
            {
                //up
                this.games.MoveKey(this.serviceGame, up.ToString());
            }

            var actualMatrix = this.game.Field;
            var expectedMatrix = new int[,]
                {
                    {0,0,4,128},
                    {0,0,32,8},
                    {0,0,0,2 },
                    {0,0,0,64}
                };

            Assert.AreEqual(expectedMatrix.GetLength(0), actualMatrix.GetLength(0));
            Assert.AreEqual(expectedMatrix.GetLength(1), actualMatrix.GetLength(1));
            Assert.AreEqual(196, this.serviceGame.CurrentScore);
            CollectionAssert.AreEqual(expectedMatrix, actualMatrix);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }

        [TestMethod]
        public void CurrentScoreReturnsCorrectValueAfterMixedKeys()
        {
            this.serviceGame.Field = new int[,]
                {
                    {8,8,16,16},
                    {2,2,16,64},
                    {0,32,32,0},
                    {16,16,16,16}
                };

            //right
            this.games.MoveKey(this.serviceGame, right.ToString());
            //up
            this.games.MoveKey(this.serviceGame, up.ToString());
            //right
            this.games.MoveKey(this.serviceGame, right.ToString());
            //down
            this.games.MoveKey(this.serviceGame, down.ToString());
            //right
            this.games.MoveKey(this.serviceGame, right.ToString());

            Assert.AreEqual(468, this.serviceGame.CurrentScore);
            Assert.AreEqual(128, this.serviceGame.MaxNumber);
        }
    }
}
