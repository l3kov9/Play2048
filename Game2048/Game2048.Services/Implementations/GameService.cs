namespace Game2048.Services.Implementations
{
    using Models;
    using System;
    using static Common.GameConstants;

    public class GameService : IGameService
    {
        public bool MoveKey(string keyCode, GameGridServiceModel game)
        {
            var direction = ReadKeyDirection(keyCode);
            var isMoved = MoveGrid(game, direction);
            FindMaxNumber(game);

            return isMoved;
        }

        private void FindMaxNumber(GameGridServiceModel game)
        {
            var maxNumber = 0;

            for (int i = 0; i < game.Field.GetLength(0); i++)
            {
                for (int k = 0; k < game.Field.GetLength(1); k++)
                {
                    if (game.Field[i, k] > maxNumber)
                    {
                        maxNumber = game.Field[i, k];
                    }
                }
            }

            game.MaxNumber = maxNumber;
        }

        private bool MoveGrid(GameGridServiceModel game, string direction)
        {
            var isMoved = false;

            switch (direction)
            {
                case "left":
                    isMoved = MoveLeft(game);
                    break;
                case "right":
                    isMoved = MoveRight(game);
                    break;
                case "up":
                    isMoved = MoveUp(game);
                    break;
                case "down":
                    isMoved = MoveDown(game);
                    break;
                default:
                    break;
            }

            return isMoved;
        }

        private bool MoveRight(GameGridServiceModel game)
        {
            var isMoved = false;

            for (int i = game.Field.GetLength(0) - 1; i >= 0; i--)
            {
                var hasPositiveNumbers = false;

                for (int k = game.Field.GetLength(1) - 2; k >= 0; k--)
                {
                    var previousNumber = game.Field[i, k + 1];
                    var currentNumber = game.Field[i, k];

                    if (currentNumber != 0)
                    {
                        hasPositiveNumbers = true;
                    }

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        game.Field[i, k + 1] = currentNumber;
                        game.Field[i, k] = 0;
                        isMoved = true;
                        k = game.Field.GetLength(1) - 1;
                    }
                }

                if (hasPositiveNumbers)
                {
                    for (int k = game.Field.GetLength(1) - 2; k >= 0; k--)
                    {
                        var previousNumber = game.Field[i, k + 1];
                        var currentNumber = game.Field[i, k];

                        if (previousNumber == currentNumber && currentNumber != 0)
                        {
                            isMoved = true;
                            game.Field[i, k + 1] = currentNumber * 2;
                            game.Field[i, k] = 0;
                            game.CurrentScore += game.Field[i, k + 1];

                            for (int j = k; j > 0; j--)
                            {
                                game.Field[i, j] = game.Field[i, j - 1];
                                game.Field[i, j - 1] = 0;
                            }
                        }
                    }
                }
            }

            return isMoved;
        }

        private bool MoveLeft(GameGridServiceModel game)
        {
            var isMoved = false;

            for (int i = 0; i < game.Field.GetLength(0); i++)
            {
                var hasPositiveNumbers = false;

                for (int k = 1; k < game.Field.GetLength(1); k++)
                {
                    var previousNumber = game.Field[i, k - 1];
                    var currentNumber = game.Field[i, k];

                    if (currentNumber != 0)
                    {
                        hasPositiveNumbers = true;
                    }

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        game.Field[i, k - 1] = currentNumber;
                        game.Field[i, k] = 0;

                        isMoved = true;
                        k = 0;
                    }
                }

                if (hasPositiveNumbers)
                {
                    for (int k = 1; k < game.Field.GetLength(1); k++)
                    {
                        var previousNumber = game.Field[i, k - 1];
                        var currentNumber = game.Field[i, k];

                        if (previousNumber == currentNumber && currentNumber != 0)
                        {
                            isMoved = true;
                            game.Field[i, k - 1] = currentNumber * 2;
                            game.Field[i, k] = 0;
                            game.CurrentScore += game.Field[i, k - 1];

                            for (int j = k; j < game.Field.GetLength(1) - 1; j++)
                            {
                                game.Field[i, j] = game.Field[i, j + 1];
                                game.Field[i, j + 1] = 0;
                            }
                        }
                    }
                }
            }

            return isMoved;
        }

        private bool MoveDown(GameGridServiceModel game)
        {
            var isMoved = false;

            for (int i = game.Field.GetLength(0) - 2; i >= 0; i--)
            {
                for (int k = game.Field.GetLength(1) - 1; k >= 0; k--)
                {
                    var previousNumber = game.Field[i + 1, k];
                    var currentNumber = game.Field[i, k];

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        game.Field[i + 1, k] = currentNumber;
                        game.Field[i, k] = 0;
                        isMoved = true;
                        i = game.Field.GetLength(0) - 2;
                        k = game.Field.GetLength(1);
                    }
                }
            }

            for (int i = game.Field.GetLength(0) - 1; i >= 0; i--)
            {
                for (int k = game.Field.GetLength(1) - 2; k >= 0; k--)
                {
                    var previousNumber = game.Field[k + 1, i];
                    var currentNumber = game.Field[k, i];

                    if (previousNumber == currentNumber && currentNumber != 0)
                    {
                        isMoved = true;
                        game.Field[k + 1, i] = currentNumber * 2;
                        game.Field[k, i] = 0;
                        game.CurrentScore += game.Field[k + 1, i];

                        for (int j = k; j > 0; j--)
                        {
                            game.Field[j, i] = game.Field[j - 1, i];
                            game.Field[j - 1, i] = 0;
                        }
                    }
                }
            }

            return isMoved;
        }

        private bool MoveUp(GameGridServiceModel game)
        {
            var isMoved = false;

            for (int i = 0; i < game.Field.GetLength(0); i++)
            {
                var hasPositiveNumbers = false;

                for (int k = 1; k < game.Field.GetLength(1); k++)
                {
                    var previousNumber = game.Field[k - 1, i];
                    var currentNumber = game.Field[k, i];

                    if (currentNumber != 0)
                    {
                        hasPositiveNumbers = true;
                    }

                    if (previousNumber == 0 && currentNumber != 0)
                    {
                        game.Field[k - 1, i] = currentNumber;
                        game.Field[k, i] = 0;
                        isMoved = true;
                        k = 0;
                    }
                }

                if (hasPositiveNumbers)
                {
                    for (int k = 1; k < game.Field.GetLength(1); k++)
                    {
                        var previousNumber = game.Field[k - 1, i];
                        var currentNumber = game.Field[k, i];

                        if (previousNumber == currentNumber && currentNumber != 0)
                        {
                            isMoved = true;
                            game.Field[k - 1, i] = currentNumber * 2;
                            game.Field[k, i] = 0;
                            game.CurrentScore += game.Field[k - 1, i];

                            for (int j = k; j < game.Field.GetLength(1) - 1; j++)
                            {
                                game.Field[j, i] = game.Field[j + 1, i];
                                game.Field[j + 1, i] = 0;
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

        public int[,] RestartGameField()
        {
            var gameGrid = new int[FieldSize, FieldSize];
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