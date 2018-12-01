namespace Game2048.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataHelpers.Constrants;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(UsernameMinLength)]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        public List<Score> Scores { get; set; } = new List<Score>();
    }
}