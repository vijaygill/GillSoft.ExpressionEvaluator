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

    public static class ExtensionMethods
    {
        public static string GetTextSafely(this RuleContext ruleContext)
        {
            var res = ruleContext != null ? ruleContext.GetText() : string.Empty;
            return res;
        }

        public static string GetTextSafely(this IToken token)
        {
            var res = token != null ? token.Text : string.Empty;
            return res;
        }

        public static string DeQuote(this string value)
        {
            var quotes = new[] { @"'", "\"" };
            var res = value;
            foreach (var quote in quotes)
            {
                if (!string.IsNullOrWhiteSpace(res))
                {
                    if (res.StartsWith(quote))
                    {
                        if (res.Length <= 1)
                        {
                            res = string.Empty;
                        }
                        else
                        {
                            res = res.Substring(1, res.Length - 1);
                        }
                    }
                    if (res.EndsWith(quote))
                    {
                        if (res.Length <= 1)
                        {
                            res = string.Empty;
                        }
                        else
                        {
                            res = res.Substring(0, res.Length - 1);
                        }
                    }
                }
            }
            return res;
        }

        public static string Beautify(this XmlDocument doc)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineChars = Environment.NewLine,
                NewLineHandling = NewLineHandling.Replace,
            };
            using (var writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            var res = sb.ToString();
            return res;
        }
    }
}
