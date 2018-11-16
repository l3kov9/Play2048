namespace Game2048.App.Models
{
    using static Common.GameConstants;

    public class GameGrid
    {
        public int[,] Field { get; set; } = new int[FieldSize, FieldSize];
    }
}
