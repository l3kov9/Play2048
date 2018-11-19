namespace Game2048.Services
{
    using Models;

    public interface IGameService
    {
        int[,] RestartGameField();

        bool MoveKey(string keyCode, GameGridServiceModel gameGrid);
    }
}
