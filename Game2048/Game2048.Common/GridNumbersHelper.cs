namespace Game2048.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using static GameConstants;

    public static class GridNumbersHelper
    {
        public static List<int> GetNonZeroNumbers(int[,] gameField)
        {
            var numbers = new List<int>();

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int k = 0; k < gameField.GetLength(1); k++)
                {
                    if (gameField[i, k] != 0)
                    {
                        numbers.Add(gameField[i, k]);
                    }
                }
            }

            return numbers;
        }

        public static List<KeyValuePair<int, int>> GetZeroIndexes(int[,] gameField)
        {
            var result = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int k = 0; k < gameField.GetLength(1); k++)
                {
                    if (gameField[i, k] == 0)
                    {
                        result.Add(new KeyValuePair<int, int>(i, k));
                    }
                }
            }

            return result;
        }

        public static string ConvertMatrixToString(int[,] field)
        {
            var result = new StringBuilder();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int k = 0; k < field.GetLength(1); k++)
                {
                    if (field[i, k] != 0)
                    {
                        result.Append(field[i, k]);
                    }

                    result.Append(',');
                }
            }

            result.Remove(result.Length - 1, 1);

            return result.ToString();
        }

        public static int[,] GetMatrix(string[] matrix)
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

        public static void AddRandomNumber(int[,] gameGrid)
        {
            var rowAndColZeroValues = GetZeroIndexes(gameGrid);

            var rnd = new Random();
            var randomRowIndex = rowAndColZeroValues[rnd.Next(0, rowAndColZeroValues.Count)];

            var row = randomRowIndex.Key;
            var col = randomRowIndex.Value;
            gameGrid[row, col] = rnd.Next(1, 101) > 20 ? 2 : 4;
        }
    }
}
