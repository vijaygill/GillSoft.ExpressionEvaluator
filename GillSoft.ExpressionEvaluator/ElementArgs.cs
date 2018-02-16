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
    public class ElementArgs
    {
        public string Prefix { get; private set; }
        public string Name { get; private set; }
        public string InnerText { get; set; }

        public ElementArgs(string prefix, string name, string innerText)
        {
            this.Prefix = prefix;
            this.Name = name;
            this.InnerText = innerText;
        }

        public ElementArgs(string prefix, string name)
            : this(prefix, name, string.Empty)
        {
        }
    }
}
