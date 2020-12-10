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
    public class XPath
    {

        #region Public Events

        public event EventHandler<AttributeArgs> OnAttribute;

        public event EventHandler<AxisArgs> OnAxis;

        public event EventHandler<ElementArgs> OnElement;
        
        public event EventHandler<NamespacePrefixArgs> OnNewPrefix;

        #endregion Public Events

        #region Public Methods

        public string CreateXml(string xpath)
        {
            var res = XPathVisitor.CreateXml(xpath);
            return res;
        }

        public void Parse(string xpath)
        {
            var visitor = new XPathVisitor((e) => this.InvokeHandler(OnElement, e),
                (e) => this.InvokeHandler(OnAttribute, e),
                (e) => this.InvokeHandler(OnNewPrefix, e),
                (e) => this.InvokeHandler(OnAxis, e)
            );
            visitor.Parse(xpath);
        }

        public XmlElement UpdateDocumentAndReturnElement(XmlDocument document, string xpath)
        {
            var res = XPathVisitor.UpdateDocumentAndReturnElement(document, xpath);
            return res;
        }

        #endregion Public Methods

    }
}
