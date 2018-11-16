namespace Game2048.Services
{
    public interface IGameService
    {
        int[,] RestartGameField(int[,] gameGrid);

        void MoveKey(string keyCode, int[,] gameGrid);
    }
}
