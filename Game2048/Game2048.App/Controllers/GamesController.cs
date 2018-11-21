namespace Game2048.App.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using Services.Models;

    using static Common.GridNumbersHelper;

    public class GamesController : Controller
    {
        private const string SessionGameBoardKeyName = "GameBoard";
        private const string SessionCurrentScoreKeyName = "CurrentScore";

        private readonly IGameService games;
        private readonly Game game;

        public GamesController(IGameService games)
        {
            this.games = games;
            this.game = new Game();
        }

        public IActionResult Index()
        {
            this.game.Field = this.games.RestartGameField();

            HttpContext.Session.SetString(SessionGameBoardKeyName, ConvertMatrixToString(this.game.Field));
            HttpContext.Session.SetInt32(SessionCurrentScoreKeyName, 0);

            return View(this.game);
        }

        [HttpPost]
        public IActionResult Index(string[] matrix, string arrowKey, int currentScore)
        {
            var currentMatrixAsString = string.Join(",", matrix);
            var sessionGameBoard = HttpContext.Session.GetString(SessionGameBoardKeyName);
            var sessionCurrentScore = HttpContext.Session.GetInt32(SessionCurrentScoreKeyName);

            if (currentMatrixAsString != sessionGameBoard || currentScore != sessionCurrentScore)
            {
                return NotFound();
            }

            var gameServiceModel = MoveGameField(matrix, arrowKey, currentScore);

            this.game.Field = gameServiceModel.Field;
            this.game.CurrentScore = gameServiceModel.CurrentScore;
            this.game.IsFinished = gameServiceModel.IsFinished;
            this.game.MaxNumber = gameServiceModel.MaxNumber;

            HttpContext.Session.SetString(SessionGameBoardKeyName, ConvertMatrixToString(this.game.Field));
            HttpContext.Session.SetInt32(SessionCurrentScoreKeyName, this.game.CurrentScore);

            return PartialView(game);
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
    }
}