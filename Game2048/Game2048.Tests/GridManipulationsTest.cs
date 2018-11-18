namespace Game2048.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
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
    }
}
