using Antlr4.Runtime;
using GillSoft.ExpressionEvaluator.Internals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator
{
    public class JsonPath
    {

        #region Public Events

        public event EventHandler<JsonPropertyArgs> OnProperty;

        public event EventHandler<JsonPropertyArgs> OnRootElement;

        #endregion Public Events

        #region Public Methods

        public JsonPathParsedResult CreateJson(string jsonPath)
        {
            var res = JsonPathVisitor.CreateJson(jsonPath);
            return res;
        }

        #endregion Public Methods

    }
}
