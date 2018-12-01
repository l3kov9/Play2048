namespace Game2048.Services.Models
{
    public class ScoreServiceModel
    {
        public string Username { get; set; }

        public int FinalScore { get; set; }

        public bool HasWon { get; set; }
    }
}
