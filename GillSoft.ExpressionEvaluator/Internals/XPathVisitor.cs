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
    internal class XPathVisitor : xpathBaseVisitor<string>, IAntlrErrorListener<IToken>
    {

        #region Private Fields

        private readonly Dictionary<string, string> namespaces = new Dictionary<string, string>();
        private readonly Action<AttributeArgs> onAttribute;
        private readonly Action<AxisArgs> onAxis;
        private readonly Action<ElementArgs> onElement;
        private readonly Action<NamespacePrefixArgs> onNewPrefix;

        private bool isRootElement = true;

        #endregion Private Fields

        #region Public Constructors

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

        #endregion Public Constructors

        #region Public Methods

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw ExtensionMethods.CreateException(offendingSymbol, msg);
        }

        public override string VisitAttribute([NotNull] xpathParser.AttributeContext context)
        {
            var res = context.name.GetTextSafely();
            return res;
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

        public override string VisitPathElement([NotNull] xpathParser.PathElementContext context)
        {
            if (context.attr != null)
            {
                var handler = onAttribute;
                if (handler != null)
                {
                    var e = new AttributeArgs(context.attr.ns.GetTextSafely(),
                        context.attr.name.GetTextSafely(), string.Empty);
                    handler(e);
                    return VisitAttribute(context.attr);
                }
            }
            if (context.elem != null)
            {
                var handler = onElement;
                if (handler != null)
                {
                    var e = new ElementArgs(context.elem.ns.GetTextSafely(), context.elem.name.GetTextSafely());
                    if (context.ax != null)
                    {
                        e.Axis = VisitAxis(context.ax);
                    }
                    if (context.filt != null)
                    {
                        var attributes = GetAttributes(context.filt).ToList();
                        
                        if (context.filt.subExpr != null)
                        {
                            attributes.AddRange(GetAttributes(context.filt.subExpr));
                        }

                        foreach (var item in attributes)
                        {
                            e.Attributes.Add(item.Name, item);
                        }
                    }
                    handler(e);
                    return base.VisitPathElement(context);
                }
            }
            return string.Empty;
        }

        public override string VisitStringDoubleQuote([NotNull] xpathParser.StringDoubleQuoteContext context)
        {
            var res = context.ChildCount == 3 ? context.GetChild(1).GetText() : context.GetTextSafely().DeQuote();
            return res;
        }

        public override string VisitStringSingleQuote([NotNull] xpathParser.StringSingleQuoteContext context)
        {
            var res = context.ChildCount == 3 ? context.GetChild(1).GetText() : context.GetTextSafely().DeQuote();
            return res;
        }

        #endregion Public Methods

        #region Private Methods

        private AttributeArgs GetAttribute(xpathParser.ExpressionContext context)
        {
            var res = new AttributeArgs(context.attr.namespacePrefix().GetTextSafely(), context.attr.name.GetTextSafely(), GetValue(context.value));
            return res;
        }

        private IEnumerable<AttributeArgs> GetAttributes(xpathParser.ExpressionContext context)
        {
            var res = new List<AttributeArgs>();

            if (context.left != null)
            {
                res.AddRange(GetAttributes(context.left));
            }

            if (context.right != null)
            {
                res.AddRange(GetAttributes(context.right));
            }

            if (context.attr != null)
            {
                res.Add(GetAttribute(context));
            }

            return res;
        }

        private string GetValue(xpathParser.StringContext context)
        {
            var res = string.Join(string.Empty, context.children.OfType<xpathParser.StringSingleQuoteContext>()
                .Select(x => VisitStringSingleQuote(x))
                .Concat(context.children.OfType<xpathParser.StringDoubleQuoteContext>()
                .Select(x => VisitStringDoubleQuote(x))));
            return res;
        }

        #endregion Private Methods

    }
}
