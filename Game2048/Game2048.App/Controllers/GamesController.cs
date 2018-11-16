namespace Game2048.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Linq;
    using System.Text;
    using static Common.GameConstants;

    public class GamesController : Controller
    {
        private readonly IGameService games;
        private GameGrid game;

        public GamesController(IGameService games)
        {
            this.games = games;
            this.game = new GameGrid();
            game.Field = this.games.RestartGameField(game.Field);
        }

        public IActionResult Index()
        {
            return View(this.game);
        }

        public IActionResult ReadKey(string keyCode)
        {
            if (keyCode == null)
            {
                var tableGrid = GetGridDataAsHtml(game.Field);

                return this.Content(tableGrid);
            }

            var matrix = ReadKeyTokens(keyCode);

            this.games.MoveKey(keyCode.Split('*').First(), matrix);

            var tableData = GetGridDataAsHtml(matrix);

            return this.Content(tableData);
        }

        private static int[,] ReadKeyTokens(string keyCode)
        {
            var tokens = keyCode.Split('*').ToList();
            tokens = tokens.Skip(1).ToList();
            var matrix = new int[FieldSize, FieldSize];

            for (int i = 0; i < tokens.Count; i++)
            {
                if (i < FieldSize)
                {
                    matrix[0, i] = int.Parse(tokens[i]);
                }
                else if (i < FieldSize * 2)
                {
                    matrix[1, i - FieldSize] = int.Parse(tokens[i]);
                }
                else if (i < FieldSize * 3)
                {
                    matrix[2, i - FieldSize * 2] = int.Parse(tokens[i]);
                }
                else
                {
                    matrix[3, i - FieldSize * 3] = int.Parse(tokens[i]);
                }
            }

            return matrix;
        }

        private string GetGridDataAsHtml(int[,] field)
        {
            var result = new StringBuilder();
            result.Append("<table class=\"table\" style=\"background-color:lightyellow\">");

            for (int i = 0; i < field.GetLength(0); i++)
            {
                result.Append("<tr>");
                for (int k = 0; k < field.GetLength(1); k++)
                {
                    var currentNumber = field[i, k];
                    var numberToView = currentNumber != 0 ? currentNumber.ToString() : string.Empty;

                    result.Append($"<td style=\"vertical-align:middle; text-shadow: 3px 2px red\">{numberToView}</td>");
                }

                result.Append("</tr>");
            }

            result.Append("</table>");

            return result.ToString();
        }
    }
}
