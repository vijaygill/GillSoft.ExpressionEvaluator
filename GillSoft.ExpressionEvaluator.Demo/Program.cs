using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                //TestParsePaths();
                //CheckXPathParserCreateNewXml();
                CheckXPathParserUpdateExistingDocument();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("Press RETURN to close...");
            Console.ReadLine();
        }

        private static void JustParsePaths()
        {
            var lines = File.ReadLines(@"XPaths.txt");
            foreach (var line in lines.Where(a => !string.IsNullOrWhiteSpace(a) && !a.StartsWith("#")))
            {
                try
                {
                    var xpath = new XPath();
                    xpath.Parse(line);
                    Console.WriteLine("Success: {0}", line);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed: {0}", line);
                    Console.WriteLine("      : {0}", ex);
                }
            }
        }

        private static void CheckXPathParserUpdateExistingDocument()
        {
            var lines = File.ReadAllLines("XPaths.txt");
            foreach (var line in lines.Where(a => !string.IsNullOrWhiteSpace(a) && !a.StartsWith("#")))
            {
                try
                {
                    var doc = new XmlDocument();
                    doc.Load(@".\SampleUpdateExistingXmlDocumentFromXPath.xml");
                    var xpath = new XPath();
                    xpath.UpdateDocumentAndReturnElement(doc, line);
                    Console.WriteLine("XPath: {0}", line);
                    Console.WriteLine(doc.Beautify());
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception thrown: {0}", ex);
                    Console.WriteLine("Input: {0}", line);
                }
            }
        }

        private static void CheckXPathParserCreateNewXml()
        {
            var lines = File.ReadAllLines("XPaths.txt");
            foreach (var line in lines.Where(a => !string.IsNullOrWhiteSpace(a) && !a.StartsWith("#")))
            {
                try
                {
                    var xpath = new XPath();
                    var xml = xpath.CreateXml(line);
                    var doc = new XmlDocument();
                    doc.LoadXml(xml);
                    Console.WriteLine("XPath: {0}", line);
                    Console.WriteLine(doc.Beautify());
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception thrown: {0}", ex);
                    Console.WriteLine("Input: {0}", line);
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
                        if (a.Name == "upper")
                        {
                            a.Result = ("" + a.Params[0].Value).ToUpper();
                        }
                        if (a.Name == "max")
                        {
                            a.Result = a.Params.Select(p => decimal.Parse("" + p.Value)).Max(p => p);
                        }
                        if (a.Name == "FuncThatReturnsTrue")
                        {
                            a.Result = true;
                        }
                        if (a.Name == "FuncThatReturnsFalse")
                        {
                            a.Result = false;
                        }
                        if (a.Name == "FuncThatReturnsNotOfParam")
                        {
                            var paramValue = "true".Equals("" + a.Params[0].Value, StringComparison.OrdinalIgnoreCase) ? true : false;
                            a.Result = !paramValue;
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
                        if ("boolFalseVar" == a.Name)
                        {
                            a.Value = false;
                        }
                        if ("boolTrueVar" == a.Name)
                        {
                            a.Value = true;
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
