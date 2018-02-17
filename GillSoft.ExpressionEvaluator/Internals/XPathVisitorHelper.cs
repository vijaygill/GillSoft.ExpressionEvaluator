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
    internal class XPathVisitorHelper
    {
        private string getNamespaceOfPrefix(string prefix, XmlDocument document)
        {
            var nsm = new XmlNamespaceManager(document.NameTable);
            var uri = nsm.LookupPrefix(prefix);
            if (string.IsNullOrWhiteSpace(uri))
            {
                uri = string.Format(@"http://example.com/namespaces/ns_{0}", prefix);
                if (string.IsNullOrWhiteSpace(prefix))
                {
                    if(string.IsNullOrWhiteSpace(document.NamespaceURI))
                    {
                        uri = string.Empty;
                    }
                    else
                    {
                        uri = @"http://example.com/namespaces/ns_default";
                    }
                }
                if (!string.IsNullOrWhiteSpace(uri))
                {
                    nsm.AddNamespace(prefix, uri);
                }
            }
            return uri;
        }

        public void UpdateDocument(XmlDocument doc, string xath)
        {
            var currentElement = doc.DocumentElement;
            var xpathVisitor = new XPath();

            var isRootElement = true;
            var canProcess = true;

            xpathVisitor.OnElement += (s, e) =>
            {
                if (!canProcess)
                {
                    return;
                }
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
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
                        var elem = doc.CreateElement(e.Prefix, e.Name, uri);
                        doc.AppendChild(elem);
                        currentElement = elem;
                    }
                    isRootElement = false;
                }
                else
                {
                    // add new element only if current element does not have a child with the same name
                    var existingChild = currentElement.ChildNodes.OfType<XmlElement>().FirstOrDefault(a => a.LocalName.Equals(e.Name));
                    if (existingChild != null)
                    {
                        // switch to that child
                        currentElement = existingChild;
                    }
                    else
                    {
                        var elem = doc.CreateElement(e.Prefix, e.Name, uri);
                        currentElement.AppendChild(elem);
                        currentElement = elem;
                    }
                }
                if (e.InnerText != null)
                {
                    currentElement.InnerText = e.InnerText;
                }
            };

            xpathVisitor.OnAttribute += (s, e) =>
            {
                if (!canProcess)
                {
                    return;
                }
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
                currentElement.SetAttribute(e.Name, uri, e.Value);
            };

            xpathVisitor.OnNewPrefix += (s, e) =>
            {
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
                doc.DocumentElement.SetAttribute("xmlns:" + e.Prefix, uri);
            };

            xpathVisitor.OnAxis += (s, e) =>
            {
                throw new Exception("Axis not supported while adding new elements.");
            };

            xpathVisitor.Parse(xath);

        }

        public string CreateXml(string xath)
        {
            var doc = new XmlDocument();
            var currentElement = default(XmlElement);

            var xpathVisitor = new XPath();
            xpathVisitor.OnElement += (s, e) =>
            {
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
                if (currentElement == null)
                {
                    var elem = doc.CreateElement(e.Prefix, e.Name, uri);
                    doc.AppendChild(elem);
                    currentElement = elem;
                }
                else
                {
                    var elem = doc.CreateElement(e.Prefix, e.Name, uri);
                    currentElement.AppendChild(elem);
                    currentElement = elem;
                }
                currentElement.InnerText = e.InnerText;
            };

            xpathVisitor.OnAttribute += (s, e) =>
            {
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
                currentElement.SetAttribute(e.Name, uri, e.Value);
            };

            xpathVisitor.OnNewPrefix += (s, e) =>
            {
                var uri = getNamespaceOfPrefix(e.Prefix, doc);
                doc.DocumentElement.SetAttribute("xmlns:" + e.Prefix, uri);
            };

            xpathVisitor.OnAxis += (s, e) =>
            {
                throw new Exception("Axis not supported while adding new elements.");
            };

            xpathVisitor.Parse(xath);

            return doc.InnerXml;
        }
    }
}
