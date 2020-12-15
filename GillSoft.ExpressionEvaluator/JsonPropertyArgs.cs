using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator
{
    public class JsonPropertyArgs
    {

        #region Internal Constructors

        internal JsonPropertyArgs(string name, int? index)
        {
            Name = name;
            Index = index;
        }

        #endregion Internal Constructors

        #region Public Properties

        public int? Index { get; }

        public string Name { get; private set; }

        #endregion Public Properties
    }
}
