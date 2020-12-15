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

        private string[] testCases = File.ReadAllLines(@"JsonPaths.txt");

        //private List<string> testCases = new List<string>
        //{
        //    {"$.store.book.title"},
        //    {"$['store']['book']['title']"},
        //    {"$['store']['book'][0]['title']"},
        //    {"$['store'][1]['location'][2]['book'][0]['title']"},
        //    {"$[2]['store'][1]['location'][2]['book'][0]['title']"},
        //    {"$.CorsOrigin[1]"},
        //    {"$.CorsOrigin.['some.property.with.dot']"},
        //    {"$.CorsOrigin.['some.property.with.dot'].Name"},
        //    {"$.CorsOrigin.['some.property.with.dot'][0]"},
        //    {"$.CorsOrigin.['some.property.with.dot'][0].Name"},
        //    {"$.CorsOrigin.['some.property.with.dot'][1]"},
        //    {"$.CorsOrigin.['some.property.with.dot'][1].Name"},
        //    {"$.CorsOrigin.['some.property.with.dot'][2]"},
        //    {"$.CorsOrigin.['some.property.with.dot'][2].Name"},
        //    {"$['store:1'][1]['location_1'][2]['book#1'][0]['title-1']"},
        //};

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
                var jsonResult = jsonPath.CreateJson(testCase);
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
