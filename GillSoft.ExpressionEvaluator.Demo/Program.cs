using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GillSoft.ExpressionEvaluator.Demo
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //CheckExpressionParser();
                CheckXPathParser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("Press RETURN to close...");
            Console.ReadLine();
        }

        private static void CheckXPathParser()
        {
            var lines = File.ReadAllLines("XPaths.txt");
            foreach (var line in lines.Where(a => !string.IsNullOrWhiteSpace(a) && !a.StartsWith("#")))
            {
                try
                {
                    var namespaces = new Dictionary<string, string>
                    {
                        { "", ""},
                        { "ns0", "http://www.gillsoft.ie/cfg/1.0"},
                        { "ns1", "http://www.gillsoft.ie/cfg/1.1"},
                    };

                    var doc = new XmlDocument();
                    var currentElement = default(XmlElement);

                    var xpath = new XPathParserVisitor();
                    xpath.OnElement += (s, e) =>
                    {
                        var uri = namespaces[e.Prefix];
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
                    };

                    xpath.OnAttribute += (s, e) =>
                    {
                        var uri = namespaces[e.Prefix];
                        currentElement.SetAttribute(e.Name, uri, e.Value);
                    };

                    xpath.OnNewPrefix += (s, e) =>
                    {
                        var uri = namespaces[e.Prefix];
                        doc.DocumentElement.SetAttribute("xmlns:" + e.Prefix, uri);
                    };

                    xpath.Parse(line);

                    Console.WriteLine("XPAth: {0}", line);
                    Console.WriteLine(doc.Beautify());

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception thrown: " + line + ":" + ex.Message);
                }
            }
        }

        private static void CheckExpressionParser()
        {
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines.Where(a => !string.IsNullOrWhiteSpace(a) && !a.StartsWith("#")))
            {
                try
                {
                    var expr = new Expression();

                    expr.HandleFunction += (sender, a) =>
                    {
                        if (a.Name == "lower")
                        {
                            a.Result = ("" + a.Params[0].Value).ToLower();
                        }
                    };

                    expr.HandleVariable += (sender, a) =>
                    {
                        if ("r" == a.Name)
                        {
                            a.Value = 5;
                        }
                        switch (a.Name)
                        {
                            case "a": { a.Value = 1; break; }
                            case "b": { a.Value = 2; break; }
                            case "c": { a.Value = 3; break; }
                            case "klm": { a.Value = 15; break; }
                            case "abc.def": { a.Value = "Some value with Caps"; break; }
                            case "#abc.def#": { a.Value = "Dublin, Ireland"; break; }
                            case "v1": { a.Value = "Dublin, Ireland"; break; }
                            //case "z": { a.Value = 415; break; }
                            default:
                                break;
                        }
                    };

                    var result = expr.Evaluate(line);

                    Console.WriteLine(line + " = " + result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception thrown: " + line + ":" + ex.Message);
                }
            }
        }
    }
}
