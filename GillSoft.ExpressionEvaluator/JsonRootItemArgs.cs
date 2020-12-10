namespace GillSoft.ExpressionEvaluator
{
    public class JsonRootItemArgs
    {

        #region Internal Constructors

        internal JsonRootItemArgs(bool isArray)
        {
            IsArray = isArray;
        }

        #endregion Internal Constructors

        #region Public Properties

        public bool IsArray { get; private set; }

        #endregion Public Properties

    }
}
