﻿namespace Game2048.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using static Common.GameConstants;

    public class GameService : IGameService
    {
        public void MoveKey(string keyCode, int[,] gameGrid)
        {
            var direction = ReadKeyDirection(keyCode);

            MoveGrid(gameGrid, direction);
        }

        private void MoveGrid(int[,] gameGrid, string direction)
        {
            switch (direction)
            {
                case "left":
                    MoveLeft(gameGrid);
                    break;
                case "right":
                    MoveRight(gameGrid);
                    break;
                case "up":
                    MoveUp(gameGrid);
                    break;
                case "down":
                    MoveDown(gameGrid);
                    break;
                default:
                    break;
            }

            AddRandomNumber(gameGrid);
        }

        private void AddRandomNumber(int[,] gameGrid)
        {
            var zeroIndexes = new List<int>();
            var index = 0;

            for (int i = 0; i < gameGrid.GetLength(0); i++)
            {
                for (int k = 0; k < gameGrid.GetLength(1); k++)
                {
                    if (gameGrid[i, k] == 0)
                    {
                        zeroIndexes.Add(index);
                    }

                    index++;
                }
            }

            var rnd = new Random();
            var indexNumberToChange = zeroIndexes[rnd.Next(0, zeroIndexes.Count)];

            var row = 0;
            var col = 0;

            if (indexNumberToChange < FieldSize)
            {
                row = 0;
                col = indexNumberToChange;
            }
            else if (indexNumberToChange < FieldSize * 2)
            {
                row = 1;
                col = indexNumberToChange - FieldSize;
            }
            else if (indexNumberToChange < FieldSize * 3)
            {
                row = 2;
                col = indexNumberToChange - FieldSize * 2;
            }
            else
            {
                row = 3;
                col = indexNumberToChange - FieldSize * 3;
            }

            gameGrid[row, col] = rnd.Next(1, 101) > 20 ? 2 : 4;
        }

        private void MoveRight(int[,] gameGrid)
        {
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

                        k = gameGrid.GetLength(1) - 1;
                    }
                }

                if (hasPositiveNumbers)
                {
                    for (int k = gameGrid.GetLength(1) - 2; k >= 0; k--)
                    {
                        var previousNumber = gameGrid[i, k + 1];
                        var currentNumber = gameGrid[i, k];

                        if (previousNumber == currentNumber)
                        {
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
        }

        private void MoveLeft(int[,] gameGrid)
        {
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
                            gameGrid[i, k - 1] = currentNumber * 2;
                            gameGrid[i, k] = 0;

                            for (int j = k; j < gameGrid.GetLength(1) - 1; j++)
                            {
                                gameGrid[i, j] = gameGrid[i, j + 1];
                            }
                        }
                    }
                }
            }
        }

        private void MoveDown(int[,] gameGrid)
        {
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
        }

        private void MoveUp(int[,] gameGrid)
        {
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