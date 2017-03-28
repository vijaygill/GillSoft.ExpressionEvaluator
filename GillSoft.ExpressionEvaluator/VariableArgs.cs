using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator
{
    public class VariableArgs : EventArgs
    {
        private object paramValue;

        public object Value
        {
            get { return paramValue; }
            set
            {
                paramValue = value;
                this.HasValue = true;
            }
        }

        public string Name { get; private set; }

        public bool HasValue { get; private set; }

        public VariableArgs(string name, object initialValue)
        {
            this.Name = name;
            this.Value = initialValue;
            this.HasValue = false;
        }
    }
}
