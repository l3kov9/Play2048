namespace Game2048.Services
{
    using Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        void AddScore(string username, int finalScore, int maxNumber);

        IEnumerable<ScoreServiceModel> GetHighScores(int count);
    }
}
