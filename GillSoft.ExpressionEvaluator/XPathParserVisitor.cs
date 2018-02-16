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

    public class XPathParserVisitor : xpathBaseVisitor<string>, IAntlrErrorListener<IToken>
    {
        public event EventHandler<ElementArgs> OnElement;
        public event EventHandler<AttributeArgs> OnAttribute;
        public event EventHandler<NamespacePrefixArgs> OnNewPrefix;
        public event EventHandler<AxisArgs> OnAxis;

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

        public override string VisitAxis([NotNull] xpathParser.AxisContext context)
        {
            var handler = OnAxis;
            if(handler!=null)
            {
                var e = new AxisArgs(context.name.Text);
                handler(this, e);
            }
            return base.VisitAxis(context);
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
}
