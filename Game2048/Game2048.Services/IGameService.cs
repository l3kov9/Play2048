namespace Game2048.Services
{
    using Models;
    using System;

    public interface IGameService
    {
        int[,] RestartGameField();

        bool MoveKey(GameServiceModel game, Enum direction);
    }
}
