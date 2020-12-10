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

        internal JsonPropertyArgs(string name)
        {
            Name = name;
        }

        #endregion Internal Constructors

        #region Public Properties

        public string Name { get; private set; }

        #endregion Public Properties

    }
}
