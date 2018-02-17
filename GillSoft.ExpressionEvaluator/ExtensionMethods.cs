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
        internal static string GetTextSafely(this RuleContext ruleContext)
        {
            var res = ruleContext != null ? ruleContext.GetText() : string.Empty;
            return res;
        }

        internal static string GetTextSafely(this IToken token)
        {
            var res = token != null ? token.Text : string.Empty;
            return res;
        }

        internal static void InvokeHandler<T> (this object sender, EventHandler<T> handler, T eventArgs)
        {
            var handlerTemp = handler;
            if(handlerTemp != null)
            {
                handlerTemp(sender, eventArgs);
            }
        }

        public static string Beautify(this XmlDocument doc)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
            };
            using (var ms = new MemoryStream())
            {
                var docTemp = new XmlDocument();
                docTemp.LoadXml(doc.OuterXml);

                using (var writer = XmlTextWriter.Create(ms, settings))
                {
                    docTemp.Save(writer);
                    var res = writer.Settings.Encoding.GetString(ms.ToArray());
                    return res;
                }
            }
        }
    }
}
