namespace Game2048.Services.Implementations
{
    using Models;
    using System;

    using static Common.Enums.Direction;
    using static Common.GameConstants;

    public class GameService : IGameService
    {
        public int[,] RestartGameField()
        {
            var gameGrid = new int[FieldSize, FieldSize];
            var hasNumberFour = false;
            var rnd = new Random();

            for (int i = 0; i < TotalRndNumbersForStart; i++)
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

        public bool MoveKey(GameServiceModel game, Enum direction)
            => MoveGrid(game, direction);

        private bool MoveGrid(GameServiceModel game, Enum direction)
        {
            var isMoved = false;

            switch (direction)
            {
                case Left:
                    isMoved = MoveLeft(game);
                    break;
                case Right:
                    isMoved = MoveRight(game);
                    break;
                case Up:
                    isMoved = MoveUp(game);
                    break;
                case Down:
                    isMoved = MoveDown(game);
                    break;
                default:
                    break;
            }

            UpdateMaxNumber(game);

            return isMoved;
        }

        private void UpdateMaxNumber(GameServiceModel game)
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

        private bool MoveRight(GameServiceModel game)
        {
            var isMoved = false;
            var field = game.Field;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                var lastPositiveValue = field[i, field.GetLength(1) - 1];
                var zeroValues = lastPositiveValue != 0 ? 0 : 1;

                for (int k = field.GetLength(1) - 2; k >= 0; k--)
                {
                    var currentNumber = field[i, k];

                    if (currentNumber != 0)
                    {
                        if (currentNumber == lastPositiveValue)
                        {
                            field[i, k + zeroValues + 1] *= 2;
                            field[i, k] = 0;
                            lastPositiveValue = 0;
                            game.CurrentScore += field[i, k + zeroValues + 1];
                            zeroValues++;
                            isMoved = true;
                        }
                        else if (zeroValues > 0)
                        {
                            field[i, k + zeroValues] = currentNumber;
                            field[i, k] = 0;
                            lastPositiveValue = currentNumber;
                            isMoved = true;
                        }
                        else
                        {
                            lastPositiveValue = currentNumber;
                        }
                    }
                    else
                    {
                        zeroValues++;
                    }
                }
            }

            return isMoved;
        }

        private bool MoveLeft(GameServiceModel game)
        {
            var isMoved = false;
            var field = game.Field;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                var lastPositiveValue = field[i, 0];
                var zeroValues = lastPositiveValue != 0 ? 0 : 1;

                for (int k = 1; k < field.GetLength(1); k++)
                {
                    var currentNumber = field[i, k];

                    if (currentNumber != 0)
                    {
                        if (currentNumber == lastPositiveValue)
                        {
                            field[i, k - zeroValues - 1] *= 2;
                            field[i, k] = 0;
                            lastPositiveValue = 0;
                            game.CurrentScore += field[i, k - zeroValues - 1];
                            zeroValues++;
                            isMoved = true;
                        }
                        else if (zeroValues > 0)
                        {
                            field[i, k - zeroValues] = currentNumber;
                            field[i, k] = 0;
                            lastPositiveValue = currentNumber;
                            isMoved = true;
                        }
                        else
                        {
                            lastPositiveValue = currentNumber;
                        }
                    }
                    else
                    {
                        zeroValues++;
                    }
                }
            }

            return isMoved;
        }

        private bool MoveDown(GameServiceModel game)
        {
            var isMoved = false;
            var field = game.Field;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                var lastPositiveValue = field[field.GetLength(1) - 1, i];
                var zeroValues = lastPositiveValue != 0 ? 0 : 1;

                for (int k = field.GetLength(1) - 2; k >= 0; k--)
                {
                    var currentNumber = field[k, i];

                    if (currentNumber != 0)
                    {
                        if (currentNumber == lastPositiveValue)
                        {
                            field[k + zeroValues + 1, i] *= 2;
                            field[k, i] = 0;
                            lastPositiveValue = 0;
                            game.CurrentScore += field[k + zeroValues + 1, i];
                            zeroValues++;
                            isMoved = true;
                        }
                        else if (zeroValues > 0)
                        {
                            field[k + zeroValues, i] = currentNumber;
                            field[k, i] = 0;
                            lastPositiveValue = currentNumber;
                            isMoved = true;
                        }
                        else
                        {
                            lastPositiveValue = currentNumber;
                        }
                    }
                    else
                    {
                        zeroValues++;
                    }
                }
            }

            return isMoved;
        }

        private bool MoveUp(GameServiceModel game)
        {
            var isMoved = false;
            var field = game.Field;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                var lastPositiveValue = field[0, i];
                var zeroValues = lastPositiveValue != 0 ? 0 : 1;

                for (int k = 1; k < field.GetLength(1); k++)
                {
                    var currentNumber = field[k, i];

                    if (currentNumber != 0)
                    {
                        if (currentNumber == lastPositiveValue)
                        {
                            field[k - zeroValues - 1, i] *= 2;
                            field[k, i] = 0;
                            lastPositiveValue = 0;
                            game.CurrentScore += field[k - zeroValues - 1, i];
                            zeroValues++;
                            isMoved = true;
                        }
                        else if (zeroValues > 0)
                        {
                            field[k - zeroValues, i] = currentNumber;
                            field[k, i] = 0;
                            lastPositiveValue = currentNumber;
                            isMoved = true;
                        }
                        else
                        {
                            lastPositiveValue = currentNumber;
                        }
                    }
                    else
                    {
                        zeroValues++;
                    }
                }
            }

            return isMoved;
        }
    }
}