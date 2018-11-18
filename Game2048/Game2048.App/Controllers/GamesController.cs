namespace Game2048.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;

    using static Common.GameConstants;

    public class GamesController : Controller
    {
        private readonly IGameService games;
        private readonly GameGrid game;

        public GamesController(IGameService games)
        {
            this.games = games;
            this.game = new GameGrid();
        }

        public IActionResult Index(string[] matrix, string arrowKey)
        {
            if(arrowKey == null)
            {
                this.game.Field = this.games.RestartGameField(null);
                return View(this.game);
            }

            var gameGrid = GetMatrix(matrix);
            var isMoved = this.games.MoveKey(arrowKey, gameGrid);
            if (isMoved)
            {
                AddRandomNumber(gameGrid);
            }
            this.game.Field = gameGrid;

            return View(game);
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

        private int[,] GetMatrix(string[] matrix)
        {
            var result = new int[FieldSize, FieldSize];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int k = 0; k < result.GetLength(1); k++)
                {
                    var numberToAdd = matrix[i * FieldSize + k];
                    result[i, k] = numberToAdd == null ? 0 : int.Parse(numberToAdd);
                }
            }

            return result;
        }
    }
}