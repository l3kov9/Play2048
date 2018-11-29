namespace Game2048.App.Controllers
{
    using Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Models;

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
            if (this.GetSessionString(SessionGameFieldKey) == null)
            {
                this.game.Field = this.gameManager.RestartGameField();

                this.SetSessionString(SessionGameFieldKey, ConvertMatrixToString(this.game.Field));
                this.SetSessionInt(SessionScoreKey, 0);
                this.SetSessionInt(SessionMaxNumKey, 0);
            }
            else
            {
                this.game.Field = GetMatrixFromString(this.GetSessionString(SessionGameFieldKey));
                this.game.CurrentScore = this.GetSessionInt(SessionScoreKey);
                this.game.MaxNumber = this.GetSessionInt(SessionMaxNumKey);
            }

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

        public IActionResult NewGame()
        {
            this.HttpContext.Session.Clear();

            return RedirectToAction(nameof(Index));
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
            var field = gameServiceModel.Field;

            for (int i = 0; i < field.GetLength(0) - 1; i++)
            {
                for (int k = 0; k < field.GetLength(1) - 1; k++)
                {
                    if (field[i, k] == field[i, k + 1] || field[i, k] == field[i + 1, k])
                    {
                        return;
                    }
                }
            }

            for (int i = 0; i < field.GetLength(0) - 1; i++)
            {
                if (field[field.GetLength(0) - 1, i] == field[field.GetLength(0) - 1, i + 1]
                    || field[i, field.GetLength(0) - 1] == field[i + 1, field.GetLength(0) - 1])
                {
                    return;
                }
            }

            gameServiceModel.IsFinished = true;
        }
    }
}