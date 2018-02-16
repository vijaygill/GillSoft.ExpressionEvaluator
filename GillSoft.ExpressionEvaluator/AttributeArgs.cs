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

    public class AttributeArgs
    {
        public string Prefix { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        public AttributeArgs(string prefix, string name, string value)
        {
            this.Prefix = prefix;
            this.Name = name;
            this.Value = value;
        }
    }
}
