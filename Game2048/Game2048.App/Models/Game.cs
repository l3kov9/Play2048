namespace Game2048.App.Models
{
    using Services.Models;
    using System.Collections.Generic;

    public class Game
    {
        public int[,] Field { get; set; }

        public int CurrentScore { get; set; }

        public bool IsFinished { get; set; }

        public int MaxNumber { get; set; }

        public IEnumerable<ScoreServiceModel> HighScores { get; set; }
    }
}
