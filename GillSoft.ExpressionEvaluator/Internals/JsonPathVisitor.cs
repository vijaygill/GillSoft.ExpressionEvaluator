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

        private readonly Action<JsonArrayItemArgs> onArrayItem;
        private readonly Action<JsonPropertyArgs> onProperty;
        private readonly Action<JsonRootItemArgs> onRootElement;

        #endregion Private Fields

        #region Public Constructors

        public JsonPathVisitor(Action<JsonRootItemArgs> onRootElement,
            Action<JsonPropertyArgs> onProperty,
            Action<JsonArrayItemArgs> onArrayItem)
        {
            this.onRootElement = onRootElement;
            this.onProperty = onProperty;
            this.onArrayItem = onArrayItem;
        }

        #endregion Public Constructors

        #region Public Methods

        public static JsonPathParsedResult CreateJson(string jsonPath)
        {
            var rootObject = default(JsonElement);

            var currentObject = default(JsonElement);

            var isTopLevelArray = false;

            Action<JsonRootItemArgs> onRootElement = (e) =>
            {
                isTopLevelArray = e.IsArray;
            };

            Action<JsonArrayItemArgs> onArrayItem = (e) =>
            {
                if (rootObject == null)
                {
                    var newItem = new JsonElement(0, string.Empty);
                    rootObject = newItem;
                    currentObject = newItem;
                }
                else
                {
                    currentObject.IsArray = true;
                }

                if (e.Index > 0)
                {
                    // add null elements only if there are more than 1 element
                    for (var i = 0; i <= e.Index; i++)
                    {
                        currentObject.Values.Add(null);
                    }
                }
            };

            Action<JsonPropertyArgs> onProperty = (e) =>
            {
                if (rootObject == null)
                {
                    var newItem = new JsonElement(0, e.Name);
                    rootObject = newItem;
                    currentObject = newItem;
                }
                else
                {
                    var newItem = new JsonElement(0, e.Name);
                    if (currentObject.IsArray)
                    {
                        // replace the last item
                        if (currentObject.Values.Any())
                        {
                            currentObject.Values.RemoveAt(currentObject.Values.Count - 1);
                        }
                        currentObject.Values.Add(newItem);
                    }
                    else
                    {
                        currentObject.Values.Add(newItem);
                    }
                    currentObject = newItem;
                }
            };

            var jsonPathImpl = new JsonPathVisitor(onRootElement, onProperty, onArrayItem);


            jsonPathImpl.Parse(jsonPath);

            var json = rootObject.GetJson(isTopLevelArray);
            var res = new JsonPathParsedResult(json, isTopLevelArray);
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
                (e) => this.InvokeHandler(this.onProperty, e),
                (e) => this.InvokeHandler(this.onArrayItem, e));

            parser.AddErrorListener(visitor);

            visitor.Visit(tree);
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw ExtensionMethods.CreateException(offendingSymbol, msg);
        }

        public override string VisitArrayIndex([NotNull] JsonPathParser.ArrayIndexContext context)
        {
            try
            {
                var handler = onArrayItem;
                if (handler != null)
                {
                    var e = new JsonArrayItemArgs(context.index.GetTextSafely().ToInt());
                    handler(e);
                }
                return base.VisitArrayIndex(context);
            }
            catch
            {
                throw ExtensionMethods.CreateException(context.GetTextSafely(), "While handling array item.");
            }
        }

        public override string VisitPropertyNameQuoted([NotNull] JsonPathParser.PropertyNameQuotedContext context)
        {
            var propertyName = context.GetTextSafely();
            try
            {
                var handler = onProperty;
                if (handler != null)
                {
                    var e = new JsonPropertyArgs(propertyName);
                    handler(e);
                }
                return propertyName;
            }
            catch
            {
                throw ExtensionMethods.CreateException(propertyName, "While handling property(quoted).");
            }
        }

        public override string VisitPropertyNameSimple([NotNull] JsonPathParser.PropertyNameSimpleContext context)
        {
            var propertyName = context.GetTextSafely();
            try
            {
                var handler = onProperty;
                if (handler != null)
                {
                    var e = new JsonPropertyArgs(propertyName);
                    handler(e);
                }
                return propertyName;
            }
            catch
            {
                throw ExtensionMethods.CreateException(propertyName, "While handling property(simple).");
            }
        }

        public override string VisitRootItemArrayItem([NotNull] JsonPathParser.RootItemArrayItemContext context)
        {
            try
            {
                var handler = onRootElement;
                if (handler != null)
                {
                    var e = new JsonRootItemArgs(true);
                    handler(e);
                }
                return base.VisitRootItemArrayItem(context);
            }
            catch
            {
                throw ExtensionMethods.CreateException(context.GetTextSafely(), "While handling array item.");
            }
        }

        public override string VisitRootItemSimple([NotNull] JsonPathParser.RootItemSimpleContext context)
        {
            try
            {
                var handler = onRootElement;
                if (handler != null)
                {
                    var e = new JsonRootItemArgs(false);
                    handler(e);
                }
                return base.VisitRootItemSimple(context);
            }
            catch
            {
                throw ExtensionMethods.CreateException(context.GetTextSafely(), "While handling array item.");
            }
        }

        #endregion Public Methods

        #region Private Classes

        private class JsonElement
        {

            #region Public Constructors

            public JsonElement(int level, string name)
            {
                this.Values = new List<JsonElement>();
                Level = level;
                Name = name;
            }

            #endregion Public Constructors

            #region Public Properties

            public bool IsArray { get; set; }

            public int Level { get; private set; }

            public string Name { get; private set; }

            public List<JsonElement> Values { get; private set; }

            #endregion Public Properties

            #region Public Methods

            public string GetJson(bool istopLevelArray)
            {

                var res = string.Empty;
                if (istopLevelArray)
                {
                    res = res + "[";
                }

                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    res = res + "{";
                    res += "\"" + this.Name + "\":";
                }

                if (IsArray)
                {
                    res += "[";
                }

                if (!this.Values.Any())
                {
                    res += "null";
                }
                else
                {

                    res += string.Join(", ", this.Values.Select(v => v == null ? "null" : v.GetJson(false)));
                }

                if (IsArray)
                {
                    res += "]";
                }


                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    res = res + "}";
                }


                if (istopLevelArray)
                {
                    res = res + "]";
                }

                return res;
            }

            public override string ToString()
            {
                return string.Format("{0} - IsArray: {1}", this.Name, this.IsArray);
            }

            #endregion Public Methods

        }

        #endregion Private Classes

    }
}
