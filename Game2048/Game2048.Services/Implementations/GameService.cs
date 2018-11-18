namespace Game2048.Services.Implementations
{
    using System;
    using static Common.GameConstants;

    public class GameService : IGameService
    {
        public bool MoveKey(string keyCode, int[,] gameGrid)
        {
            var direction = ReadKeyDirection(keyCode);

            var isMoved = MoveGrid(gameGrid, direction);

            return isMoved;
        }

        private bool MoveGrid(int[,] gameGrid, string direction)
        {
            var isMoved = false;

            switch (direction)
            {
                case "left":
                    isMoved = MoveLeft(gameGrid);
                    break;
                case "right":
                    isMoved = MoveRight(gameGrid);
                    break;
                case "up":
                    isMoved = MoveUp(gameGrid);
                    break;
                case "down":
                    isMoved = MoveDown(gameGrid);
                    break;
                default:
                    break;
            }

            return isMoved;
        }
        
        private bool MoveRight(int[,] gameGrid)
        {
            var isMoved = false;

            for (int i = gameGrid.GetLength(0) - 1; i >= 0; i--)
            {
                var hasPositiveNumbers = false;

                for (int k = gameGrid.GetLength(1) - 2; k >= 0; k--)
                {
                    var previousNumber = gameGrid[i, k + 1];
                    var currentNumber = gameGrid[i, k];

                    if (currentNumber != 0)
                    {
                        hasPositiveNumbers = true;
                    }

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        gameGrid[i, k + 1] = currentNumber;
                        gameGrid[i, k] = 0;
                        isMoved = true;
                        k = gameGrid.GetLength(1) - 1;
                    }
                }

                if (hasPositiveNumbers)
                {
                    for (int k = gameGrid.GetLength(1) - 2; k >= 0; k--)
                    {
                        var previousNumber = gameGrid[i, k + 1];
                        var currentNumber = gameGrid[i, k];

                        if (previousNumber == currentNumber && currentNumber != 0)
                        {
                            isMoved = true;
                            gameGrid[i, k + 1] = currentNumber * 2;
                            gameGrid[i, k] = 0;

                            for (int j = k; j > 0; j--)
                            {
                                gameGrid[i, j] = gameGrid[i, j - 1];
                                gameGrid[i, j - 1] = 0;
                            }
                        }
                    }
                }
            }

            return isMoved;
        }

        private bool MoveLeft(int[,] gameGrid)
        {
            var isMoved = false;

            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                var hasPositiveNumbers = false;

                for (int k = 1; k < gameGrid.GetLength(1); k++)
                {
                    var previousNumber = gameGrid[i, k - 1];
                    var currentNumber = gameGrid[i, k];

                    if (currentNumber != 0)
                    {
                        hasPositiveNumbers = true;
                    }

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        gameGrid[i, k - 1] = currentNumber;
                        gameGrid[i, k] = 0;

                        isMoved = true;
                        k = 0;
                    }
                }

                if (hasPositiveNumbers)
                {
                    for (int k = 1; k < gameGrid.GetLength(1); k++)
                    {
                        var previousNumber = gameGrid[i, k - 1];
                        var currentNumber = gameGrid[i, k];

                        if (previousNumber == currentNumber && currentNumber != 0)
                        {
                            isMoved = true;
                            gameGrid[i, k - 1] = currentNumber * 2;
                            gameGrid[i, k] = 0;

                            for (int j = k; j < gameGrid.GetLength(1) - 1; j++)
                            {
                                gameGrid[i, j] = gameGrid[i, j + 1];
                                gameGrid[i, j + 1] = 0;
                            }
                        }
                    }
                }
            }

            return isMoved;
        }

        private bool MoveDown(int[,] gameGrid)
        {
            var isMoved = false;

            for (int i = gameGrid.GetLength(0) - 2; i >= 0; i--)
            {
                for (int k = gameGrid.GetLength(1) - 1; k >= 0; k--)
                {
                    var previousNumber = gameGrid[i + 1, k];
                    var currentNumber = gameGrid[i, k];

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        gameGrid[i + 1, k] = currentNumber;
                        gameGrid[i, k] = 0;
                        isMoved = true;
                        i = gameGrid.GetLength(0) - 2;
                        k = gameGrid.GetLength(1);
                    }
                }
            }

            for (int i = gameGrid.GetLength(0) - 1; i >= 0; i--)
            {
                for (int k = gameGrid.GetLength(1) - 2; k >= 0; k--)
                {
                    var previousNumber = gameGrid[k + 1, i];
                    var currentNumber = gameGrid[k, i];

                    if (previousNumber == currentNumber && currentNumber != 0)
                    {
                        isMoved = true;
                        gameGrid[k + 1, i] = currentNumber * 2;
                        gameGrid[k, i] = 0;

                        for (int j = k; j > 0; j--)
                        {
                            gameGrid[j, i] = gameGrid[j - 1, i];
                            gameGrid[j - 1, i] = 0;
                        }
                    }
                }
            }

            return isMoved;
        }

        private bool MoveUp(int[,] gameGrid)
        {
            var isMoved = false;

            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                var hasPositiveNumbers = false;

                for (int k = 1; k < gameGrid.GetLength(1); k++)
                {
                    var previousNumber = gameGrid[k - 1, i];
                    var currentNumber = gameGrid[k, i];

                    if (currentNumber != 0)
                    {
                        hasPositiveNumbers = true;
                    }

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        gameGrid[k - 1, i] = currentNumber;
                        gameGrid[k, i] = 0;
                        isMoved = true;
                        k = 0;
                    }
                }

                if (hasPositiveNumbers)
                {
                    for (int k = 1; k < gameGrid.GetLength(1); k++)
                    {
                        var previousNumber = gameGrid[k - 1, i];
                        var currentNumber = gameGrid[k, i];

                        if (previousNumber == currentNumber && currentNumber != 0)
                        {
                            isMoved = true;
                            gameGrid[k - 1, i] = currentNumber * 2;
                            gameGrid[k, i] = 0;

                            for (int j = k; j < gameGrid.GetLength(1) - 1; j++)
                            {
                                gameGrid[j, i] = gameGrid[j + 1, i];
                                gameGrid[j + 1, i] = 0;
                            }
                        }
                    }
                }
            }

            return isMoved;
        }

        private static string ReadKeyDirection(string keyCode)
        {
            var direction = string.Empty;

            switch (keyCode)
            {
                case "37":
                    direction = "left";
                    break;
                case "38":
                    direction = "up";
                    break;
                case "39":
                    direction = "right";
                    break;
                case "40":
                    direction = "down";
                    break;
                default:
                    break;
            }

            return direction;
        }

        public int[,] RestartGameField(int[,] gameGrid)
        {
            gameGrid = new int[FieldSize, FieldSize];
            var hasNumberFour = false;
            var rnd = new Random();

            for (int i = 0; i < 2; i++)
            {
                var x = rnd.Next(FieldSize);
                var y = rnd.Next(FieldSize);

                if (gameGrid[x, y] == 0)
                {
                    if (rnd.Next(1, 101) > 20)
                    {
                        gameGrid[x, y] = 2;
                    }
                    else
                    {
                        gameGrid[x, y] = hasNumberFour ? 2 : 4;
                        hasNumberFour = true;
                    }
                }
                else
                {
                    i--;
                }
            }

            return gameGrid;
        }
    }
}