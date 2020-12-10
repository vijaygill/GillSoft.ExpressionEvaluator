using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GillSoft.ExpressionEvaluator
{
    public class JsonPathParsedResult
    {

        #region Internal Constructors

        internal JsonPathParsedResult(string json, bool isTopLevelArray)
        {
            Json = json;
            IsTopLevelArray = isTopLevelArray;
        }

        #endregion Internal Constructors

        #region Public Properties

        public bool IsTopLevelArray { get; private set; }

        public string Json { get; private set; }

        #endregion Public Properties

    }
}
