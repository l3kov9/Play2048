﻿namespace Game2048.Tests
{
    using App.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Implementations;

    [TestClass]
    public class ArrowKeysTest
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
                    {2,0,2,128},
                    {8,8,8,0},
                    {8,8,2,0},
                    {16,16,16,16}
                }
            };
        }

        [TestMethod]
        public void RightArrowGetsCorrectValues()
        {
            this.games.MoveKey("39", this.game.Field);
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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void MultipleRightArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey("39", this.game.Field);
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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void LeftArrowGetsCorrectValues()
        {
            this.games.MoveKey("37", game.Field);

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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void MultipleLeftArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey("37", game.Field);
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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void UpArrowGetsCorrectValues()
        {
            this.games.MoveKey("38", game.Field);

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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void MultipleUpArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey("38", game.Field);
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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void DownArrowGetsCorrectValues()
        {
            this.games.MoveKey("40", game.Field);

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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void MultipleDownArrowGetsCorrectValues()
        {
            for (int i = 0; i < 20; i++)
            {
                this.games.MoveKey("40", game.Field);
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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }

        [TestMethod]
        public void MixedArrowKeysGetsCorrectValues()
        {
            //left
            this.games.MoveKey("37", game.Field);

            //down
            this.games.MoveKey("40", game.Field);

            //left
            this.games.MoveKey("37", game.Field);

            //right
            this.games.MoveKey("39", game.Field);

            for (int i = 0; i < 5; i++)
            {
                //up
                this.games.MoveKey("38", game.Field);
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

            for (int i = 0; i < actualMatrix.GetLength(0); i++)
            {
                for (int k = 0; k < actualMatrix.GetLength(1); k++)
                {
                    Assert.AreEqual(expectedMatrix[i, k], actualMatrix[i, k]);
                }
            }
        }
    }
}
