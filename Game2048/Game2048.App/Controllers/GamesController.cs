namespace Game2048.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Models;
    using System;

    using static Common.GameConstants;
    using static Common.GridNumbersHelper;

    public class GamesController : Controller
    {
        private readonly IGameService games;
        private readonly Game game;

        public GamesController(IGameService games)
        {
            this.games = games;
            this.game = new Game();
        }

        public ActionResult Index()
        {
            this.game.Field = this.games.RestartGameField();
            return View(this.game);
        }

        [HttpPost]
        public IActionResult Index(string[] matrix, string arrowKey, int currentScore)
        {
            var gameServiceModel = MoveGameField(matrix, arrowKey, currentScore);

            this.game.Field = gameServiceModel.Field;
            this.game.CurrentScore = gameServiceModel.CurrentScore;
            this.game.IsFinished = gameServiceModel.IsFinished;

            return View(game);
        }

        private GameGridServiceModel MoveGameField(string[] matrix, string arrowKey, int currentScore)
        {
            var gameServiceModel = new GameGridServiceModel()
            {
                Field = GetMatrix(matrix),
                CurrentScore = currentScore
            };

            var isMoved = this.games.MoveKey(arrowKey, gameServiceModel);

            if (isMoved)
            {
                AddRandomNumber(gameServiceModel.Field);
            }
            else
            {
                CheckIfGameIsOver(gameServiceModel);
            }

            return gameServiceModel;
        }

        private void CheckIfGameIsOver(GameGridServiceModel gameServiceModel)
        {
            var zeroValuesLength = GetZeroIndexes(gameServiceModel.Field).Count;

            if (zeroValuesLength == 0)
            {
                var testGameField = new GameGridServiceModel()
                {
                    Field = new int[gameServiceModel.Field.GetLength(0), gameServiceModel.Field.GetLength(1)],
                    CurrentScore = gameServiceModel.CurrentScore
                };

                for (int i = 0; i < gameServiceModel.Field.GetLength(0); i++)
                {
                    for (int k = 0; k < gameServiceModel.Field.GetLength(1); k++)
                    {
                        testGameField.Field[i, k] = gameServiceModel.Field[i, k];
                    }
                }

                for (int i = 37; i <= 40; i++)
                {
                    this.games.MoveKey(i.ToString(), testGameField);
                }

                var testGameNonZeroValuesLength = GetZeroIndexes(testGameField.Field).Count;

                if (testGameNonZeroValuesLength == 0)
                {
                    gameServiceModel.IsFinished = true;
                }
            }
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