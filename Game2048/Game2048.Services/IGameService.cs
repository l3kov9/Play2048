namespace Game2048.Services
{
    using Models;
    using System;

    public interface IGameService
    {
        int[,] RestartGameField();

        bool MoveKey(GameGridServiceModel game, Enum direction);
    }
}
