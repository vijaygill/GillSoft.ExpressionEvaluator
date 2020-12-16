using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator.Internals
{
    internal class JsonPathVisitor : JsonPathBaseVisitor<string>, IAntlrErrorListener<IToken>
    {

        #region Private Fields

        private readonly Action<JsonPropertyArgs> onProperty;
        private readonly Action<JsonPropertyArgs> onRootElement;

        #endregion Private Fields

        #region Public Constructors

        public JsonPathVisitor(Action<JsonPropertyArgs> onRootElement,
            Action<JsonPropertyArgs> onProperty)
        {
            this.onRootElement = onRootElement;
            this.onProperty = onProperty;
        }

        #endregion Public Constructors

        #region Public Methods

        public static JsonPathParsedResult CreateJson(string jsonPath)
        {
            var rootObject = default(JsonPathElement);

            var currentObject = default(JsonPathElement);

            Action<JsonPropertyArgs> onRootElement = (e) =>
            {
                var newItem = new JsonPathElement(string.Empty, e.Index);
                rootObject = newItem;
                currentObject = newItem;
            };

            Action<JsonPropertyArgs> onProperty = (e) =>
            {
                var newItem = new JsonPathElement(e.Name, e.Index);
                currentObject.Value = newItem;
                currentObject = newItem;
            };

            var jsonPathImpl = new JsonPathVisitor(onRootElement, onProperty);

            jsonPathImpl.Parse(jsonPath);

            var json = rootObject.GetJson();
            var res = new JsonPathParsedResult(json, rootObject.Index.HasValue);
            return res;
        }

        public void Parse(string jsonPath)
        {
            var inputStream = new AntlrInputStream(jsonPath);
            var lexer = new JsonPathLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new JsonPathParser(tokenStream);

            parser.RemoveErrorListeners();

            var tree = parser.jsonpath();

            var visitor = new JsonPathVisitor(
                (e) => this.InvokeHandler(this.onRootElement, e),
                (e) => this.InvokeHandler(this.onProperty, e));

            parser.AddErrorListener(visitor);

            visitor.Visit(tree);
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw ExtensionMethods.CreateException(offendingSymbol, msg);
        }

        public override string VisitArrayIndex([NotNull] JsonPathParser.ArrayIndexContext context)
        {
            var res = context.index.GetTextSafely();
            return res;
        }

        public override string VisitProperty([NotNull] JsonPathParser.PropertyContext context)
        {
            var propertyName = context.propertyWithDot != null
                ? GetPropertyWithDotName(context.propertyWithDot.GetTextSafely())
                : context.propertyInBrackets != null
                ? GetPropertyWithBracketsName(context.propertyInBrackets.GetTextSafely())
                : string.Empty;

            try
            {
                var index = context.index != null
                    ? this.VisitArrayIndex(context.index).ToInt()
                    : default(int?);

                var handler = onProperty;
                if (handler != null)
                {
                    var e = new JsonPropertyArgs(propertyName, index);
                    handler(e);
                }
                return propertyName;
            }
            catch
            {
                throw ExtensionMethods.CreateException(propertyName, "While handling property.");
            }
        }

        public override string VisitRootItem([NotNull] JsonPathParser.RootItemContext context)
        {
            var propertyName = string.Empty;

            try
            {
                var index = context.index != null
                    ? this.VisitArrayIndex(context.index).ToInt()
                    : default(int?);

                var handler = onRootElement;
                if (handler != null)
                {
                    var e = new JsonPropertyArgs(propertyName, index);
                    handler(e);
                }
                return base.VisitRootItem(context);
            }
            catch
            {
                throw ExtensionMethods.CreateException(propertyName, "While handling root item.");
            }
        }

        #endregion Public Methods

        #region Private Methods

        private string GetPropertyWithBracketsName(string name)
        {
            var stringsToRemoveInBeginning = new[]
            {
                @"['"
            };
            var stringsToRemoveInEnd = new[]
            {
                @"']"
            };

            var res = name;

            if (!string.IsNullOrWhiteSpace(name))
            {
                foreach (var item in stringsToRemoveInBeginning)
                {
                    if (name.StartsWith(item))
                    {
                        name = name.Substring(item.Length);
                    }
                }
                foreach (var item in stringsToRemoveInEnd)
                {
                    if (name.EndsWith(item))
                    {
                        name = name.Substring(0, name.Length - item.Length);
                    }
                }
                res = name;
            }
            return res;
        }

        private string GetPropertyWithDotName(string name)
        {
            var stringsToRemoveInBeginning = new[]
            {
                @"."
            };
            var res = name;

            if (!string.IsNullOrWhiteSpace(name))
            {
                foreach (var item in stringsToRemoveInBeginning)
                {
                    if (name.StartsWith(item))
                    {
                        name = name.Substring(item.Length);
                    }
                }
                res = name;
            }
            return res;
        }

        #endregion Private Methods

        #region Private Classes

        private class JsonPathElement
        {

            #region Public Constructors

            public JsonPathElement(string name, int? index)
            {
                Name = name;
                Index = index;
            }

            #endregion Public Constructors

            #region Public Properties

            public int? Index { get; private set; }

            public string Name { get; private set; }
            public JsonPathElement Value { get; set; }

            #endregion Public Properties

            #region Public Methods

            public string GetJson()
            {
                var res = string.Empty;

                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    res += "{";
                    res += "\"" + this.Name + "\":";
                }

                if (this.Index.HasValue)
                {
                    res += "[";
                    if (Index.Value > 0)
                    {
                        res += string.Join(", ", Enumerable.Range(0, this.Index.Value).Select(x => "null"))
                            + ",";
                    }
                }

                if (Value == null)
                {
                    res += "null";
                }
                else
                {
                    res += Value.GetJson();
                }

                if (this.Index.HasValue)
                {
                    res += "]";
                }

                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    res += "}";
                }

                return res;
            }

            public override string ToString()
            {
                return string.Format("{0} - IsArray: {1}", this.Name, this.Index.HasValue);
            }

            #endregion Public Methods

        }

        #endregion Private Classes

    }
}
