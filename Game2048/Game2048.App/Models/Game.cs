namespace Game2048.App.Models
{
    public class Game
    {
        public int[,] Field { get; set; }

        public int CurrentScore { get; set; }

        public bool IsFinished { get; set; }

        public int MaxNumber { get; set; }
    }
}
