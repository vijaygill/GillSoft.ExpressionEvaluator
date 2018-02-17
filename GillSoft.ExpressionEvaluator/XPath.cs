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
    public class XPath : IAntlrErrorListener<IToken>
    {
        public event EventHandler<ElementArgs> OnElement;
        public event EventHandler<AttributeArgs> OnAttribute;
        public event EventHandler<NamespacePrefixArgs> OnNewPrefix;
        public event EventHandler<AxisArgs> OnAxis;

        public void Parse(string xpath)
        {
            var inputStream = new AntlrInputStream(xpath);
            var lexer = new xpathLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new xpathParser(tokenStream);

            parser.RemoveErrorListeners();

            parser.AddErrorListener(this);

            var tree = parser.path();

            var visitor = new XPathVisitor((e) => this.InvokeHandler(OnElement, e),
                (e) => this.InvokeHandler(OnAttribute, e),
                (e) => this.InvokeHandler(OnNewPrefix, e),
                (e) => this.InvokeHandler(OnAxis, e)
            );

            visitor.Visit(tree);
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw ExtensionMethods.CreateException(offendingSymbol, msg);
        }


        public void UpdateDocument(XmlDocument document, string xpath)
        {
            var helper = new XPathVisitorHelper();
            helper.UpdateDocument(document, xpath);
        }

        public string CreateXml(string xpath)
        {
            var helper = new XPathVisitorHelper();
            var res = helper.CreateXml(xpath);
            return res;
        }
    }
}
