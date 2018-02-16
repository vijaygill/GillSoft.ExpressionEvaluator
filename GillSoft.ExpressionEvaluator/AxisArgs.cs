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

    public class AxisArgs
    {
        public string Name { get; private set; }

        public AxisArgs(string name)
        {
            this.Name = name;
        }
    }
}
