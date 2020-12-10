namespace GillSoft.ExpressionEvaluator
{
    public class JsonArrayItemArgs
    {

        #region Internal Constructors

        internal JsonArrayItemArgs(int index)
        {
            Index = index;
        }

        #endregion Internal Constructors

        #region Public Properties

        public int Index { get; private set; }

        #endregion Public Properties

    }
}
