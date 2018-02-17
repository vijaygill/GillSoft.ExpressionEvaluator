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
using System.Diagnostics;

namespace GillSoft.ExpressionEvaluator
{

    public class XPathVisitor : xpathBaseVisitor<string>, IAntlrErrorListener<IToken>
    {
        private readonly Action<ElementArgs> onElement;
        private readonly Action<AttributeArgs> onAttribute;
        private readonly Action<NamespacePrefixArgs> onNewPrefix;
        private readonly Action<AxisArgs> onAxis;

        public XPathVisitor(Action<ElementArgs> onElement,
         Action<AttributeArgs> onAttribute,
         Action<NamespacePrefixArgs> onNewPrefix,
         Action<AxisArgs> onAxis)
        {
            this.onElement = onElement;
            this.onAttribute = onAttribute;
            this.onNewPrefix = onNewPrefix;
            this.onAxis = onAxis;
        }

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
                var handler = onNewPrefix;
                if (handler != null)
                {
                    if (!this.namespaces.ContainsKey(prefix))
                    {
                        var e = new NamespacePrefixArgs(prefix);
                        handler(e);
                        this.namespaces.Add(e.Prefix, e.Uri);
                    }
                }
            }
        }

        public override string VisitAxis([NotNull] xpathParser.AxisContext context)
        {
            var handler = onAxis;
            if (handler != null)
            {
                var e = new AxisArgs(context.name.Text);
                handler(e);
            }
            return base.VisitAxis(context);
        }

        public override string VisitFilter([NotNull] xpathParser.FilterContext context)
        {
            if (context.attr != null)
            {
                var handler = onAttribute;
                if (handler != null)
                {
                    var e = new AttributeArgs(context.attr.ns.GetTextSafely(),
                        context.attr.name.GetTextSafely(), VisitString(context.value));
                    handler(e);
                    RaiseNamespacePriefix(e.Prefix);
                }
            }
            if (context.elem != null)
            {
                var handler = onElement;
                if (handler != null)
                {
                    var innerText = VisitString(context.value);
                    if (string.IsNullOrWhiteSpace(innerText))
                    {
                        return VisitElement(context.elem);
                    }
                    else
                    {
                        var e = new ElementArgs(context.elem.ns.GetTextSafely(), context.elem.name.GetTextSafely(), innerText);
                        handler(e);
                        RaiseNamespacePriefix(e.Prefix);
                    }
                }
            }
            return base.VisitFilter(context);
        }

        public override string VisitElement([NotNull] xpathParser.ElementContext context)
        {
            var handler = onElement;
            if (handler != null)
            {
                var e = new ElementArgs(context.ns.GetTextSafely(), context.name.GetTextSafely());
                handler(e);
                RaiseNamespacePriefix(e.Prefix);
            }
            return base.VisitElement(context);
        }

        public override string VisitStringDoubleQuote([NotNull] xpathParser.StringDoubleQuoteContext context)
        {
            var res = context.ChildCount == 3 ? context.GetChild(1).GetText() : context.GetTextSafely();
            return res;
        }

        public override string VisitStringSingleQuote([NotNull] xpathParser.StringSingleQuoteContext context)
        {
            var res = context.ChildCount == 3 ? context.GetChild(1).GetText() : context.GetTextSafely();
            return res;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw Expression.CreateException(offendingSymbol, msg);
        }
    }
}
