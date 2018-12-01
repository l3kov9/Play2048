namespace Game2048.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataHelpers.Constrants;

    public class Score
    {
        public int Id { get; set; }

        [Range((double)FinalScoreMinValue, FinalScoreMaxValue)]
        public int FinalScore { get; set; }

        [Range((double)MaxNumberMinValue, MaxNumberMaxValue)]
        public int MaxNumber { get; set; }

        public bool HasWon => MaxNumber == 2048;

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
