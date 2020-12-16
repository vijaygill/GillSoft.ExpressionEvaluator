using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GillSoft.ExpressionEvaluator.Tests
{
    [TestClass]
    public class JsonPathUnitTest
    {
        #region Private Fields

        private string[] testCases = File.ReadAllLines(@"JsonPaths.txt")
            .Where(line => !line.StartsWith("#")).ToArray();

        #endregion Private Fields

        #region Public Methods

        [TestMethod]
        public void TestJsonPathsInNewObject()
        {
            //arrange
            var expr = new Expression();

            //act

            //assert
            foreach (var testCase in testCases)
            {
                var jsonPath = new JsonPath();
                var jsonResult = jsonPath.CreateJson(testCase, false);
                JContainer obj = jsonResult.IsTopLevelArray ? JArray.Parse(jsonResult.Json) as JContainer : JObject.Parse(jsonResult.Json) as JContainer;
                var selectedTokens = obj.SelectTokens(testCase).ToList();
                Assert.AreEqual(1, selectedTokens.Count);
                foreach (var item in selectedTokens)
                {
                    item.Replace("apples");
                }
                Assert.IsTrue(obj.ToString().Contains("apples"), "Test failed for: " + testCase);
            }
        }

        #endregion Public Methods

    }
}
