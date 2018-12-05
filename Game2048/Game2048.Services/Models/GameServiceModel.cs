namespace Game2048.Services.Models
{
    public class GameServiceModel
    {
        public int[,] Field { get; set; }

        public int CurrentScore { get; set; }

        public bool IsFinished { get; set; }

        public int MaxNumber { get; set; }
    }
}
