namespace Game2048.App.Models
{
    using Services.Models;
    using System.Collections.Generic;

    public class GameViewModel
    {
        public int[,] Field { get; set; }

        public int CurrentScore { get; set; }

        public int MaxNumber { get; set; }

        public bool IsFinished { get; set; }

        public IEnumerable<ScoreServiceModel> HighScores { get; set; }
    }
}
