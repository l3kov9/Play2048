namespace Game2048.App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
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
            var isMoved = this.games.MoveKey(keyCode.Split('*').First(), matrix);
            if (isMoved)
            {
                AddRandomNumber(matrix);
            }

            var tableData = GetGridDataAsHtml(matrix);

            return this.Content(tableData);
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
