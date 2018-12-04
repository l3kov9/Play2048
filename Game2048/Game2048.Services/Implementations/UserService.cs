namespace Game2048.Services.Implementations
{
    using Data;
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly Game2048DbContext db;

        public UserService(Game2048DbContext db)
        {
            this.db = db;
        }

        public void AddScore(string username, int finalScore, int maxNumber)
        {
            var userId = this.db.Users
                .Where(u => u.Username == username)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == 0)
            {
                this.db.Users.Add(new User() { Username = username });
                this.db.SaveChanges();
            }

            this.db.Scores.Add(new Score
            {
                FinalScore = finalScore,
                MaxNumber = maxNumber,
                UserId = this.db.Users.Where(u=>u.Username == username).First().Id
            });

            this.db.SaveChanges();
        }

        public IEnumerable<ScoreServiceModel> GetHighScores(int count)
            => this.db
                .Scores
                .OrderByDescending(sc => sc.HasWon)
                .ThenByDescending(sc => sc.FinalScore)
                .Select(sc => new ScoreServiceModel
                {
                    Username = sc.User.Username,
                    FinalScore = sc.FinalScore,
                    HasWon = sc.HasWon
                })
                .Take(count)
                .ToList();
    }
}
