using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using System.IO;
using Antlr4.Runtime.Tree;
using System.Xml;

namespace GillSoft.ExpressionEvaluator
{
    public class ElementArgs
    {
        public string Prefix { get; private set; }
        public string Name { get; private set; }
        public string InnerText { get; set; }

        public ElementArgs(string prefix, string name, string innerText)
        {
            this.Prefix = prefix;
            this.Name = name;
            this.InnerText = innerText;
        }

        public ElementArgs(string prefix, string name)
            : this(prefix, name, string.Empty)
        {
        }
    }

    public class NamespacePrefixArgs
    {
        public string Prefix { get; private set; }
        public string Uri { get; private set; }

        public NamespacePrefixArgs(string prefix)
        {
            this.Prefix = prefix;
        }
    }

    public class AttributeArgs
    {
        public string Prefix { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        public AttributeArgs(string prefix, string name, string value)
        {
            this.Prefix = prefix;
            this.Name = name;
            this.Value = value;
        }
    }

    public class XPathParserVisitor : xpathBaseVisitor<string>, IAntlrErrorListener<IToken>
    {
        public event EventHandler<ElementArgs> OnElement;
        public event EventHandler<AttributeArgs> OnAttribute;
        public event EventHandler<NamespacePrefixArgs> OnNewPrefix;

        private readonly Dictionary<string, string> namespaces = new Dictionary<string, string>();

        public void Parse(string xpath)
        {
            var inputStream = new AntlrInputStream(xpath);
            var lexer = new xpathLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new xpathParser(tokenStream);

            parser.RemoveErrorListeners();

            parser.AddErrorListener(this);

            var tree = parser.path();

            var res = this.Visit(tree);
        }

        private void RaiseNamespacePriefix(string prefix)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                var handler = OnNewPrefix;
                if (handler != null)
                {
                    if (!this.namespaces.ContainsKey(prefix))
                    {
                        var e = new NamespacePrefixArgs(prefix);
                        handler(this, e);
                        this.namespaces.Add(e.Prefix, e.Uri);
                    }
                }
            }
        }

        public override string VisitFunction([NotNull] xpathParser.FunctionContext context)
        {
            return base.VisitFunction(context);
        }

        public override string VisitFilter([NotNull] xpathParser.FilterContext context)
        {
            if (context.attr != null)
            {
                var handler = OnAttribute;
                if (handler != null)
                {
                    var e = new AttributeArgs(context.attr.ns.GetTextSafely(),
                        context.attr.name.GetTextSafely(), context.value.GetTextSafely().DeQuote());
                    handler(this, e);
                    RaiseNamespacePriefix(e.Prefix);
                }
            }
            if (context.elem != null)
            {
                var handler = OnElement;
                if (handler != null)
                {
                    var e = new ElementArgs(context.elem.ns.GetTextSafely(), context.elem.name.GetTextSafely(), context.value.GetTextSafely().DeQuote());
                    handler(this, e);
                    RaiseNamespacePriefix(e.Prefix);
                }
            }
            return base.VisitFilter(context);
        }

        public override string VisitElement([NotNull] xpathParser.ElementContext context)
        {
            var handler = OnElement;
            if (handler != null)
            {
                var e = new ElementArgs(context.ns.GetTextSafely(), context.name.GetTextSafely());
                handler(this, e);
                RaiseNamespacePriefix(e.Prefix);
            }
            return base.VisitElement(context);
        }


        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw Expression.CreateException(offendingSymbol, msg);
        }
    }

    public static class ExtensionMethods
    {
        public static string GetTextSafely(this RuleContext ruleContext)
        {
            var res = ruleContext != null ? ruleContext.GetText() : string.Empty;
            return res;
        }

        public static string GetTextSafely(this IToken token)
        {
            var res = token != null ? token.Text : string.Empty;
            return res;
        }

        public static string DeQuote(this string value)
        {
            var quotes = new[] { @"'", "\"" };
            var res = value;
            foreach (var quote in quotes)
            {
                if (!string.IsNullOrWhiteSpace(res))
                {
                    if (res.StartsWith(quote))
                    {
                        if (res.Length <= 1)
                        {
                            res = string.Empty;
                        }
                        else
                        {
                            res = res.Substring(1, res.Length - 1);
                        }
                    }
                    if (res.EndsWith(quote))
                    {
                        if (res.Length <= 1)
                        {
                            res = string.Empty;
                        }
                        else
                        {
                            res = res.Substring(0, res.Length - 1);
                        }
                    }
                }
            }
            return res;
        }

        public static string Beautify(this XmlDocument doc)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = Environment.NewLine,
                NewLineHandling = NewLineHandling.Replace,
            };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            var res = sb.ToString();
            return res;
        }
    }
}
