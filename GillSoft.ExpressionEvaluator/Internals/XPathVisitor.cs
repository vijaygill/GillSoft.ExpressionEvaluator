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

        private bool isRootElement = true;

        private void RaiseNamespacePrefixEvent(string prefix)
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
            try
            {
                var handler = onAxis;
                if (handler != null)
                {
                    var e = new AxisArgs(context.name.Text);
                    handler(e);
                }
                return base.VisitAxis(context);
            }
            catch
            {
                throw ExtensionMethods.CreateException(context.name, "While handling axis.");
            }
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
                    RaiseNamespacePrefixEvent(e.Prefix);
                    handler(e);
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
                        RaiseNamespacePrefixEvent(e.Prefix);
                        handler(e);
                    }
                }
            }
            return base.VisitFilter(context);
        }

        public override string VisitElement([NotNull] xpathParser.ElementContext context)
        {
            try
            {
                var handler = onElement;
                if (handler != null)
                {
                    var e = new ElementArgs(context.ns.GetTextSafely(), context.name.GetTextSafely());
                    if (isRootElement)
                    {
                        handler(e);
                        RaiseNamespacePrefixEvent(e.Prefix);
                    }
                    else
                    {
                        RaiseNamespacePrefixEvent(e.Prefix);
                        handler(e);
                    }
                }
                return base.VisitElement(context);
            }
            finally
            {
                isRootElement = false;
            }
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
            throw ExtensionMethods.CreateException(offendingSymbol, msg);
        }
    }
}
