using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GillSoft.ExpressionEvaluator.Tests
{
    [TestClass]
    public class ExpressionUnitTest
    {
        [TestMethod]
        public void AddPositiveToPositive()
        {
            //arrange
            var expr = new Expression();

            //act
            var result = expr.Evaluate("10 + 15");

            //assert
            Assert.AreEqual((double)25, result);
        }

        [TestMethod]
        public void AddPositiveToNegative()
        {
            //arrange
            var expr = new Expression();

            //act
            var result = expr.Evaluate("10 + -15");

            //assert
            Assert.AreEqual((double)-5, result);
        }

        [TestMethod]
        public void NegativeToNegative()
        {
            //arrange
            var expr = new Expression();

            //act
            var result = expr.Evaluate("-10 + -15");

            //assert
            Assert.AreEqual((double)-25, result);
        }

        [TestMethod]
        public void NegativeToPositive()
        {
            //arrange
            var expr = new Expression();

            //act
            var result = expr.Evaluate("-10 + 15");

            //assert
            Assert.AreEqual((double)5, result);
        }

    }
}
