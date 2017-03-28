using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator
{
    public class FunctionArgs : EventArgs
    {
        public List<FunctionParameter> Params { get; private set; }
        public string Name { get; private set; }

        private object result;
        public object Result { get { return result; } set { result = value; HasResult = true; } }

        public bool HasResult { get; private set; }

        public FunctionArgs(string name)
        {
            this.Params = new List<FunctionParameter>();
            this.Name = name;
            this.HasResult = false;
        }
    }
}
