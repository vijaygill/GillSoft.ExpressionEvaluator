using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator.Internals
{
    internal class JsonPathVisitor : JsonPathBaseVisitor<string>
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

        public static JsonPathParsedResult CreateJson(string jsonPath, bool formatted)
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

            var json = rootObject.GetJson(formatted);
            var res = new JsonPathParsedResult(json, rootObject.Index.HasValue);
            return res;
        }

        public override string VisitArrayIndex([NotNull] JsonPathParser.ArrayIndexContext context)
        {
            var res = GetArrayIndex(context.GetTextSafely());
            return res;
        }

        public override string VisitProperty([NotNull] JsonPathParser.PropertyContext context)
        {
            var propertyName = VisitPropertyType(context.prop);
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
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
            return base.VisitProperty(context);
        }

        public override string VisitPropertyType([NotNull] JsonPathParser.PropertyTypeContext context)
        {
            var propertyName = context.propertyWithDotAndBracket != null
                            ? VisitPropertyWithDotAndBracketRule(context.propertyWithDotAndBracket)
                            : context.propertyWithDot != null
                            ? VisitPropertyWithDotRule(context.propertyWithDot)
                            : context.propertyWithBrackets != null
                            ? VisitPropertyWithBracketsRule(context.propertyWithBrackets)
                            : string.Empty;
            return propertyName;
        }

        public override string VisitPropertyWithBracketsRule([NotNull] JsonPathParser.PropertyWithBracketsRuleContext context)
        {
            var propertyName = context.GetTextSafely();
            var res = GetPropertyWithBracketsName(propertyName);
            return res;
        }

        public override string VisitPropertyWithDotAndBracketRule([NotNull] JsonPathParser.PropertyWithDotAndBracketRuleContext context)
        {
            var propertyName = context.GetTextSafely();
            var res = GetPropertyWithDotAndBracketsName(propertyName);
            return res;
        }

        public override string VisitPropertyWithDotRule([NotNull] JsonPathParser.PropertyWithDotRuleContext context)
        {
            var propertyName = context.GetTextSafely();
            var res = GetPropertyWithDotName(propertyName);
            return res;
        }

        public override string VisitRootItem([NotNull] JsonPathParser.RootItemContext context)
        {
            var x = context.GetTextSafely();
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

        private string GetArrayIndex(string text)
        {
            var stringsToRemoveInBeginning = new[]
            {
                @"["
            };
            var stringsToRemoveInEnd = new[]
            {
                @"]"
            };

            var res = text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                foreach (var item in stringsToRemoveInBeginning)
                {
                    if (text.StartsWith(item))
                    {
                        text = text.Substring(item.Length);
                    }
                }
                foreach (var item in stringsToRemoveInEnd)
                {
                    if (text.EndsWith(item))
                    {
                        text = text.Substring(0, text.Length - item.Length);
                    }
                }
                res = text;
            }
            return res;
        }

        private string GetPropertyWithBracketsName(string text)
        {
            var stringsToRemoveInBeginning = new[]
            {
                @"['"
            };
            var stringsToRemoveInEnd = new[]
            {
                @"']"
            };

            var res = text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                foreach (var item in stringsToRemoveInBeginning)
                {
                    if (text.StartsWith(item))
                    {
                        text = text.Substring(item.Length);
                    }
                }
                foreach (var item in stringsToRemoveInEnd)
                {
                    if (text.EndsWith(item))
                    {
                        text = text.Substring(0, text.Length - item.Length);
                    }
                }
                res = text;
            }
            return res;
        }

        private string GetPropertyWithDotAndBracketsName(string text)
        {
            var stringsToRemoveInBeginning = new[]
            {
                @".['"
            };
            var stringsToRemoveInEnd = new[]
            {
                @"']"
            };

            var res = text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                foreach (var item in stringsToRemoveInBeginning)
                {
                    if (text.StartsWith(item))
                    {
                        text = text.Substring(item.Length);
                    }
                }
                foreach (var item in stringsToRemoveInEnd)
                {
                    if (text.EndsWith(item))
                    {
                        text = text.Substring(0, text.Length - item.Length);
                    }
                }
                res = text;
            }
            return res;
        }

        private string GetPropertyWithDotName(string text)
        {
            var stringsToRemoveInBeginning = new[]
            {
                @"."
            };
            var res = text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                foreach (var item in stringsToRemoveInBeginning)
                {
                    if (text.StartsWith(item))
                    {
                        text = text.Substring(item.Length);
                    }
                }
                res = text;
            }
            return res;
        }

        private void Parse(string jsonPath)
        {
            var inputStream = new AntlrInputStream(jsonPath);
            var lexer = new JsonPathLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new JsonPathParser(tokenStream);

            var visitor = new JsonPathVisitor(this.onRootElement, this.onProperty);

            var errorHandler = new ErrorHandler(jsonPath);

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(errorHandler);

            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorHandler);

            var tree = parser.jsonpath();
            visitor.Visit(tree);
        }

        #endregion Private Methods

        #region Private Classes

        private class ErrorHandler : IAntlrErrorListener<IToken>, IAntlrErrorListener<int>
        {
            #region Private Fields

            private readonly string input;

            #endregion Private Fields

            #region Public Constructors

            public ErrorHandler(string input)
            {
                this.input = input;
            }

            #endregion Public Constructors

            #region Public Methods

            void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                throw ExtensionMethods.CreateException(offendingSymbol, msg);
            }

            void IAntlrErrorListener<int>.SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
            {
                var message = string.Format("Error at Line {0} Position {1}: {2}", line, charPositionInLine, msg);
                throw ExtensionMethods.CreateException(input, message);
            }

            #endregion Public Methods

        }

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

            public string GetJson(bool formatted)
            {
                var sb = new StringBuilderPlus(formatted, 0);

                this.AppendJson(sb);

                var res = sb.ToString().Trim();

                return res;
            }

            public override string ToString()
            {
                return string.Format("{0} - IsArray: {1}", this.Name, this.Index.HasValue);
            }

            #endregion Public Methods

            #region Private Methods

            private void AppendJson(StringBuilderPlus stringBuilder)
            {
                if (!string.IsNullOrWhiteSpace(this.Name))
                {
                    stringBuilder.Append("\"" + this.Name + "\": ");
                }

                if (Index.HasValue)
                {
                    stringBuilder.AppendLine("[");
                }

                if (Index.HasValue)
                {
                    using (stringBuilder.BeginIndent())
                    {
                        for (var i = 0; i < Index.Value; i++)
                        {
                            stringBuilder.AppendLine("null,");
                        }
                    }
                }
                if (Value != null)
                {
                    using (var level = (Index.HasValue ? stringBuilder.BeginIndent() : stringBuilder.BeginNoIndent()))
                    {
                        stringBuilder.AppendLine("{");
                        using (stringBuilder.BeginIndent())
                        {
                            Value.AppendJson(stringBuilder);
                        }
                        stringBuilder.AppendLine("}");
                    }
                }
                else
                {
                    using (var level = (Index.HasValue ? stringBuilder.BeginIndent() : stringBuilder.BeginNoIndent()))
                    {
                        stringBuilder.AppendLine("null");
                    }
                }

                if (Index.HasValue)
                {
                    stringBuilder.AppendLine("]");
                }
            }

            #endregion Private Methods

        }

        #endregion Private Classes

    }
}
