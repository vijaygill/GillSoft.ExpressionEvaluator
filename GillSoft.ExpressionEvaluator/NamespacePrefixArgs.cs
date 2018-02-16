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

    public class NamespacePrefixArgs
    {
        public string Prefix { get; private set; }
        public string Uri { get; private set; }

        public NamespacePrefixArgs(string prefix)
        {
            this.Prefix = prefix;
        }
    }
}
