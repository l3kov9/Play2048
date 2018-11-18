namespace Game2048.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;

    using static Common.GameConstants;
    using static Common.GridNumbersHelper;

    public class GamesController : Controller
    {
        private readonly IGameService games;
        private readonly GameGrid game;

        public GamesController(IGameService games)
        {
            this.games = games;
            this.game = new GameGrid();
        }

        public ActionResult Index(string[] matrix, string arrowKey)
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
            var rowAndColZeroValues = GetZeroIndexes(gameGrid);
            var rnd = new Random();
            var randomRowIndex = rowAndColZeroValues[rnd.Next(0, rowAndColZeroValues.Count)];

            var row = randomRowIndex.Key;
            var col = randomRowIndex.Value;
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