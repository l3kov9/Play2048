namespace Game2048.Common
{
    using System.Collections.Generic;

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
    }
}
