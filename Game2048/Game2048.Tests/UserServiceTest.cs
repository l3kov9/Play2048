namespace Game2048.Tests
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services.Implementations;
    using System;
    using System.Linq;

    [TestClass]
    public class UserServiceTest
    {
        private Game2048DbContext db;
        private UserService userManager;

        [TestInitialize]
        public void SetInit()
        {
            this.db = this.GetDatabase();
            this.userManager = new UserService(db);
        }

        [TestMethod]
        public void GetHighScoresReturnsEmptyListIfNoScoresInDb()
        {
            var result = this.userManager.GetHighScores(10);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void GetHighScoresReturnsCorrectResultWithCorrectValues()
        {
            const int count = 10;

            SeedData();
            var result = this.userManager.GetHighScores(count);

            Assert.AreEqual(count, result.Count());
            Assert.IsTrue(result.ElementAt(0).HasWon == result.ElementAt(2).HasWon
                && result.ElementAt(2).HasWon != result.ElementAt(3).HasWon);
        }

        [TestMethod]
        public void AddScoreShouldSaveCorrectDataWithValidValues()
        {
            const string username = "Pesho1";
            const int finalScore = 12264;
            const int maxNumber = 2048;

            SeedData();

            this.userManager.AddScore(username, finalScore, maxNumber);
            var savedEntry = this.db.Scores.Last();

            Assert.AreEqual(username, savedEntry.User.Username);
            Assert.AreEqual(finalScore, savedEntry.FinalScore);
            Assert.AreEqual(maxNumber, savedEntry.MaxNumber);
        }

        private Game2048DbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<Game2048DbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new Game2048DbContext(dbOptions);
        }

        private void SeedData()
        {
            for (int i = 0; i < 5; i++)
            {
                var user = new User()
                {
                    Username = $"Pesho{i + 1}"
                };

                this.db.Add(user);
            }

            this.db.SaveChanges();

            for (int i = 0; i < 20; i++)
            {
                var score = new Score()
                {
                    FinalScore = i * 100,
                    MaxNumber = i % 5 == 0 ? 2048 : i * 20,
                    UserId = i % 6
                };

                this.db.Add(score);
            }

            this.db.SaveChanges();
        }
    }
}
