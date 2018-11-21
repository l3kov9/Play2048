namespace Game2048.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    using static Common.GameConstants;
    using static Common.GridNumbersHelper;

    [TestClass]
    public class GridManipulationsTest
    {
        private int[,] grid;

        [TestInitialize]
        public void SetInit()
        {
            this.grid = new int[,]
                {
                    {2,0,2,128},
                    {0,8,8,0},
                    {8,8,0,0},
                    {0,16,0,16}
                 };
        }

        [TestMethod]
        public void GetNonZeroValuesReturnsCorrectValues()
        {
            var nonZeroValues = GetNonZeroNumbers(this.grid);
            var expectedValues = new List<int>() { 2, 2, 128, 8, 8, 8, 8, 16, 16 };

            CollectionAssert.AreEqual(expectedValues, nonZeroValues);
        }

        [TestMethod]
        public void MatrixConvertsToStringReturnsCorrectValue()
        {
            var matrixToString = ConvertMatrixToString(this.grid);

            Assert.AreEqual("2,,2,128,,8,8,,8,8,,,,16,,16", matrixToString);
        }

        [TestMethod]
        public void ConvertArrayToMatrixReturnsCorrectMultiArray()
        {
            var singleArray = new string[] { null, "2", "32", null, "64", "32", null, null, null, "16", "128", "12", "2", "2", null, null };

            var multiArray = GetMatrix(singleArray);
            var expectedMatrix = new[,]
            {
                {0, 2, 32, 0 },
                {64, 32, 0, 0 },
                {0, 16, 128, 12 },
                { 2, 2, 0, 0 }
            };

            CollectionAssert.AreEqual(expectedMatrix, multiArray);
        }

        [TestMethod]
        public void RndNumberAddExactlyOneNumberTwoOrFour()
        {
            var field = new int[FieldSize, FieldSize];

            for (int i = 0; i < 100; i++)
            {
                AddRandomNumber(field);
                var sum = 0;
                var addedNumsCount = 0;

                for (int k = 0; k < field.GetLength(0); k++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (field[k, j] != 0)
                        {
                            sum += field[k, j];
                            addedNumsCount++;
                        }
                    }
                }

                Assert.AreEqual(1, addedNumsCount);
                Assert.IsTrue(sum == 2 || sum == 4);

                field = new int[FieldSize, FieldSize];
            }
        }
    }
}
