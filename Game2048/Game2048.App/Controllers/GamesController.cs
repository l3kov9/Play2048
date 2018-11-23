namespace Game2048.App.Controllers
{
    using Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Models;

    using static Common.GameConstants;
    using static Common.GameFieldHelpers;

    public class GamesController : Controller
    {
        private const string SessionGameFieldKey = "GameField";
        private const string SessionScoreKey = "CurrentScore";
        private const string SessionMaxNumKey = "MaxNumber";

        private readonly IGameService gameManager;
        private readonly Game game;

        public GamesController(IGameService gameManager)
        {
            this.gameManager = gameManager;
            this.game = new Game();
        }

        public IActionResult Index()
        {
            this.game.Field = this.gameManager.RestartGameField();

            this.SetSessionString(SessionGameFieldKey, ConvertMatrixToString(this.game.Field));
            this.SetSessionInt(SessionScoreKey, 0);
            this.SetSessionInt(SessionMaxNumKey, 0);

            return View(this.game);
        }

        [HttpPost]
        public IActionResult Index(string direction)
        {
            var gameField = GetMatrixFromString(this.GetSessionString(SessionGameFieldKey));
            var currentScore = this.GetSessionInt(SessionScoreKey);
            var gameServiceModel = MoveGameField(gameField, currentScore, direction);

            this.game.Field = gameServiceModel.Field;
            this.game.CurrentScore = gameServiceModel.CurrentScore;
            this.game.IsFinished = gameServiceModel.IsFinished;
            this.game.MaxNumber = gameServiceModel.MaxNumber;

            this.SetSessionString(SessionGameFieldKey, ConvertMatrixToString(this.game.Field));
            this.SetSessionInt(SessionScoreKey, this.game.CurrentScore);
            this.SetSessionInt(SessionMaxNumKey, this.game.MaxNumber);

            return PartialView("_GameBoardPartial", game);
        }

        private GameGridServiceModel MoveGameField(int[,] gameField, int currentScore, string direction)
        {
            var gameServiceModel = new GameGridServiceModel()
            {
                Field = gameField,
                CurrentScore = currentScore
            };

            var isMoved = this.gameManager.MoveKey(gameServiceModel, direction);

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
                var testGameField = new int[gameServiceModel.Field.GetLength(0), gameServiceModel.Field.GetLength(1)];

                for (int i = 0; i < testGameField.GetLength(0); i++)
                {
                    for (int k = 0; k < testGameField.GetLength(1); k++)
                    {
                        testGameField[i, k] = gameServiceModel.Field[i, k];
                    }
                }

                var testGame = new GameGridServiceModel()
                {
                    Field = testGameField,
                    CurrentScore = gameServiceModel.CurrentScore
                };

                foreach (var direction in Directions)
                {
                    this.gameManager.MoveKey(testGame, direction);
                }

                var testGameNonZeroValuesLength = GetZeroIndexes(testGame.Field).Count;
                if (testGameNonZeroValuesLength == 0)
                {
                    gameServiceModel.IsFinished = true;
                }
            }
        }
    }
}