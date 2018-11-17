namespace Game2048.Services
{
    public interface IGameService
    {
        int[,] RestartGameField(int[,] gameGrid);

        bool MoveKey(string keyCode, int[,] gameGrid);
    }
}
