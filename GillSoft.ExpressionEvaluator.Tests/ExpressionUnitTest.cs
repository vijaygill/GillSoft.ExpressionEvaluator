using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GillSoft.ExpressionEvaluator.Tests
{
    [TestClass]
    public class ExpressionUnitTest
    {
        private Dictionary<string, bool> testCasesBoolean = new Dictionary<string, bool>
        {
            {"true || true", true},
            {"true || false", true},
            {"false || true", true},
            {"false || false", false},
            {"true && true", true},
            {"true && false", false},
            {"false && true", false},
            {"false && false", false},
            {"true && (false || true)", true},
            {"true && !(false || true)", false},

            {"true && !(false || NotFunc(true))", true},
        };

        private Dictionary<string, double> testCasesNumeric = new Dictionary<string, double>
        {
            {"10 + 15", 25},
            {"10 + -15", -5},
            {"-10 + 15", 5},
            {"-10 + -15", -25},

            {"10 - 15", -5},
            {"10 - -15", 25},
            {"-10 - 15", -25},
            {"-10 - -15", 5},

            {"10 * 15", 150},
            {"10 * -15", -150},
            {"-10 * 15", -150},
            {"-10 * -15", 150},

            {"20 / 5", 4},
            {"20 / -5", -4},
            {"-20 / 5", -4},
            {"-20 / -5", 4},

            { "25 * 4 / 2 + 10 - 4 ", 56},

            { "25 * 4 / (2 + 10 - 8) ", 25},

        };

        private Dictionary<string, string> testCasesString = new Dictionary<string, string>
        {
            { "'Hello' + ' ' + 'World'", "Hello World"},
            { "\"Hello\" + ' ' + \"World\"", "Hello World"},
        };

        [TestMethod]
        public void TestNumericExpressions()
        {
            //arrange
            var expr = new Expression();

            //act

            //assert
            foreach (var testCase in testCasesNumeric)
            {
                var res = expr.Evaluate(testCase.Key);
                var resDecimal = (double)res;
                Assert.AreEqual(testCase.Value, resDecimal, "Testcase failed for expression: " + testCase.Key);
            }
        }

        [TestMethod]
        public void TestStringExpressions()
        {
            //arrange
            var expr = new Expression();

            //act

            //assert
            foreach (var testCase in testCasesString)
            {
                var res = "" + expr.Evaluate(testCase.Key);
                Assert.AreEqual(testCase.Value, res, "Testcase failed for expression: " + testCase.Key);
            }
        }

        [TestMethod]
        public void TestBooleanExpressions()
        {
            //arrange
            var expr = new Expression();
            expr.HandleFunction += (sender, a) =>
            {
                if ("NotFunc".Equals(a.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var paramValue = "true".Equals("" + a.Params[0].Value, StringComparison.OrdinalIgnoreCase) ? true : false;
                    a.Result = !paramValue;
                }
            };

            //act

            //assert
            foreach (var testCase in testCasesBoolean)
            {
                var res = (bool)expr.Evaluate(testCase.Key);
                Assert.AreEqual(testCase.Value, res, "Testcase failed for expression: " + testCase.Key);
            }
        }
    }
}
