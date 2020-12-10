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
        private string getDefaultNamespace(XmlDocument doc)
        {
            var res = doc.DocumentElement == null ? string.Empty
                : doc.DocumentElement.Attributes.OfType<XmlAttribute>().Where(a => a.LocalName.Equals("xmlns")).Select(a => a.Value).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(res))
            {
                res = string.Empty;
            }
            return res;
        }

        private string getNamespaceOfPrefix(string prefix, XmlDocument document)
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

        public XmlElement UpdateDocumentAndReturnElement(XmlDocument doc, string xath)
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

            return currentElement;
        }

        public string CreateXml(string xpath)
        {
            var doc = new XmlDocument();

            UpdateDocumentAndReturnElement(doc, xpath);

            return doc.InnerXml;
        }
    }
}
