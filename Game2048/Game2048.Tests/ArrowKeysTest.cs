namespace Game2048.Tests
{
    using App.Models;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;
    using Services.Implementations;
    using System.Linq;

    [TestClass]
    public class ArrowKeysTest
    {
        private readonly IGameService games;

        public ArrowKeysTest()
        {
            this.games = new GameService();
        }

        [TestMethod]
        public void RightArrowGetsCorrectValues()
        {
            var game = new GameGrid
            {
                Field = new int[,]
                {
                    {1,0,2,1},
                    {0,1,0,0},
                    {1,1,2,0},
                    {0,1,0,1}
                }
            };

            this.games.MoveKey("39", game.Field);

            Assert.AreEqual(new int[,]
                {
                    {0,1,2,1},
                    {0,0,0,1},
                    {0,0,2,2},
                    {0,0,0,2}
                }, game.Field);
        }

        [TestMethod]
        public void LeftArrowGetsCorrectValues()
        {
            var game = new GameGrid
            {
                Field = new int[,]
                {
                    {1,0,2,1},
                    {0,1,0,0},
                    {1,1,2,0},
                    {0,1,0,1}
                }
            };

            this.games.MoveKey("37", game.Field);

            Assert.AreEqual(new int[,]
                {
                    {1,2,1,0},
                    {1,0,0,0},
                    {2,2,0,0},
                    {2,0,0,0}
                }, game.Field);
        }

        [TestMethod]
        public void UpArrowGetsCorrectValues()
        {
            var game = new GameGrid
            {
                Field = new int[,]
                {
                    {1,0,2,1},
                    {0,1,0,0},
                    {1,1,2,0},
                    {0,1,0,1}
                }
            };

            this.games.MoveKey("38", game.Field);

            Assert.AreEqual(new int[,]
                {
                    {2,2,4,2},
                    {0,1,0,0},
                    {0,0,0,0},
                    {0,0,0,0}
                }, game.Field);
        }

        [TestMethod]
        public void DownArrowGetsCorrectValues()
        {
            var game = new GameGrid
            {
                Field = new int[,]
                {
                    {1,0,2,1},
                    {0,1,0,0},
                    {1,1,2,0},
                    {0,1,0,1}
                }
            };

            this.games.MoveKey("40", game.Field);

            Assert.AreEqual(new int[,]
                {
                    {0,0,0,0},
                    {0,0,0,0},
                    {0,1,0,0},
                    {2,2,4,2}
                }, game.Field);
        }
    }
}
