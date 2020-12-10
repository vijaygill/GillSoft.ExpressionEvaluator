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

        public static string CreateXml(string xpath)
        {
            var doc = new XmlDocument();

            UpdateDocumentAndReturnElement(doc, xpath);

            return doc.InnerXml;
        }

        public static XmlElement UpdateDocumentAndReturnElement(XmlDocument doc, string xath)
        {
            var currentElement = doc.DocumentElement;

            var isRootElement = true;
            var canProcess = true;

            Action<ElementArgs> onElement = (e) =>
            {
                if (!canProcess)
                {
                    return;
                }
                if (isRootElement)
                {
                    if (doc.DocumentElement != null)
                    {
                        // process further elements only if root element matches
                        canProcess = string.Equals(e.Name, doc.DocumentElement.LocalName);
                        currentElement = doc.DocumentElement;
                    }
                    else
                    {
                        // root element was null, so append new element to document
                        var uri = getNamespaceOfPrefix(e.Prefix, doc);
                        var elem = uri.Equals(doc.NamespaceURI) ? doc.CreateElement(e.Name)
                            : doc.CreateElement(e.Prefix, e.Name, uri);
                        doc.AppendChild(elem);
                        currentElement = elem;
                    }
                    isRootElement = false;
                }
                else
                {
                    // add new element only if current element does not have a child with the same name
                    var existingChild = currentElement.ChildNodes.OfType<XmlElement>()
                        .FirstOrDefault(a => a.LocalName.Equals(e.Name) && (!e.Attributes.Any() || a.Attributes.OfType<XmlAttribute>().Any(attr => e.Attributes.ContainsKey(attr.LocalName) && e.Attributes[attr.LocalName].Value.Equals(attr.Value))));
                    if (existingChild != null)
                    {
                        // switch to that child
                        currentElement = existingChild;
                    }
                    else
                    {
                        var uri = getNamespaceOfPrefix(e.Prefix, doc);
                        var elem = doc.CreateElement(e.Prefix, e.Name, uri);

                        if (uri.Equals(getDefaultNamespace(doc)))
                        {
                            elem = doc.CreateElement(string.Empty, e.Name, getDefaultNamespace(doc));
                            var nsm = new XmlNamespaceManager(doc.NameTable);
                            nsm.AddNamespace(e.Prefix, uri);
                        }
                        foreach (var attr in e.Attributes)
                        {
                            var attrUri = getNamespaceOfPrefix(attr.Value.Prefix, doc);
                            var newAttr = elem.OwnerDocument.CreateAttribute(attr.Value.Prefix, attr.Key, attrUri);
                            newAttr.Value = attr.Value.Value;
                            elem.Attributes.Append(newAttr);
                        }

                        currentElement.AppendChild(elem);
                        currentElement = elem;
                    }
                }
                if (e.InnerText != null)
                {
                    currentElement.InnerText = e.InnerText;
                }
            };

            Action<AttributeArgs> onAttribute = (e) =>
            {
                if (!canProcess)
                {
                    return;
                }
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
                currentElement.SetAttribute(e.Name, uri, e.Value);
            };

            Action<NamespacePrefixArgs> onNewPrefix = (e) =>
            {
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
                doc.DocumentElement.SetAttribute("xmlns:" + e.Prefix, uri);
            };

            Action<AxisArgs> onAxis = (e) =>
            {
                throw new Exception("Axis not supported while adding new elements.");
            };

            var xpathVisitor = new XPathVisitor(
                (e) => onElement(e),
                (e) => onAttribute(e),
                (e) => onNewPrefix(e),
                (e) => onAxis(e)
                );


            xpathVisitor.Parse(xath);

            return currentElement;
        }

        public void Parse(string xpath)
        {
            var inputStream = new AntlrInputStream(xpath);
            var lexer = new xpathLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new xpathParser(tokenStream);

            var tree = parser.path();

            var visitor = new XPathVisitor(onElement, onAttribute, onNewPrefix, onAxis);

            parser.RemoveErrorListeners();

            parser.AddErrorListener(visitor);

            visitor.Visit(tree);
        }

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

        private static string getDefaultNamespace(XmlDocument doc)
        {
            var res = doc.DocumentElement == null ? string.Empty
                : doc.DocumentElement.Attributes.OfType<XmlAttribute>().Where(a => a.LocalName.Equals("xmlns")).Select(a => a.Value).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(res))
            {
                res = string.Empty;
            }
            return res;
        }

        private static string getNamespaceOfPrefix(string prefix, XmlDocument document)
        {
            var desiredAttrName = string.IsNullOrWhiteSpace(prefix) ? "xmlns" : "xmlns:" + prefix;
            var nsm = new XmlNamespaceManager(document.NameTable);
            var uri = nsm.LookupNamespace(prefix);
            if (string.IsNullOrWhiteSpace(uri) && document.DocumentElement != null)
            {
                // try looking using attribute names
                var xmlnsAttr = document.DocumentElement.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => a.LocalName.Equals("xmlns"));
                if (xmlnsAttr != null)
                {
                    uri = xmlnsAttr.Value;
                }
            }

            if (string.IsNullOrWhiteSpace(uri))
            {
                if (string.IsNullOrWhiteSpace(prefix))
                {
                    if (string.IsNullOrWhiteSpace(document.NamespaceURI))
                    {
                        uri = string.Empty;
                    }
                    else
                    {
                        uri = @"http://example.com/namespaces/ns_default";
                    }
                }
                else
                {
                    uri = @"http://example.com/namespaces/ns_" + prefix;
                }
            }

            if (!string.IsNullOrWhiteSpace(prefix) && !string.IsNullOrWhiteSpace(uri))
            {
                nsm.AddNamespace(prefix, uri);
                if (document.DocumentElement != null)
                {
                    var attr = document.DocumentElement.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => a.Name.Equals(desiredAttrName));
                    if (attr == null)
                    {
                        attr = document.CreateAttribute(desiredAttrName);
                        attr.Value = uri;
                        document.DocumentElement.Attributes.Append(attr);
                    }
                }
            }

            return uri;
        }

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
