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

        public event EventHandler<JsonArrayItemArgs> OnArrayItem;

        public event EventHandler<JsonPropertyArgs> OnProperty;

        public event EventHandler<JsonRootItemArgs> OnRootElement;

        #endregion Public Events

        #region Public Methods

        public JsonPathParsedResult CreateJson(string jsonPath)
        {
            var res = JsonPathVisitor.CreateJson(jsonPath);
            return res;
        }

        public void Parse(string jsonPath)
        {
            var visitor = new JsonPathVisitor(
                (e) => this.InvokeHandler(this.OnRootElement, e),
                (e) => this.InvokeHandler(this.OnProperty, e),
                (e) => this.InvokeHandler(this.OnArrayItem, e));
            visitor.Parse(jsonPath);
        }

        #endregion Public Methods

    }
}
